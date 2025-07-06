using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using SPTS_Writer.Entities;
using SPTS_Writer.Eventbus.Publishers;

namespace SPTS_Writer.Eventbus.ViewChanges
{
    public class UserView
    {
        private readonly IMongoDatabase _database;
        private readonly UsersChangePublish _usersChangePublish;

        public UserView(IMongoDatabase database, UsersChangePublish usersChangePublish)
        {
            _database = database;
            _usersChangePublish = usersChangePublish;
        }

        // Example method to get the Users collection

        public async Task CreateAllTestsViewAsync()
        {
            try
            {
                var pipeline = new BsonDocument[0]; // Empty pipeline to include all documents and fields
                await _database.CreateViewAsync<BsonDocument, BsonDocument>("UserView", "user", pipeline);
                Console.WriteLine("View 'allTestsView' created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating view: {ex.Message}");
            }
        }

        public async Task SyncUserSnapshotWithUsersAsync(CancellationToken cancellationToken)
        {
            var snapshotCollection = _database.GetCollection<User>("UserView");
            if(snapshotCollection == null)
            {
                await CreateAllTestsViewAsync();
            }
            var usersCollection = _database.GetCollection<User>("Users");

            var snapshotDocs = await snapshotCollection.Find(FilterDefinition<User>.Empty).ToListAsync();
            var usersDocs = await usersCollection.Find(FilterDefinition<User>.Empty).ToListAsync();

            var snapshotByEmail = snapshotDocs
                .Where(u => !string.IsNullOrEmpty(u.Email))
                .ToDictionary(u => u.Email.ToLowerInvariant(), u => u);

            var usersByEmail = usersDocs
                .Where(u => !string.IsNullOrEmpty(u.Email))
                .ToDictionary(u => u.Email.ToLowerInvariant(), u => u);

            // Detect creates
            foreach (var email in usersByEmail.Keys.Except(snapshotByEmail.Keys))
            {
                var user = usersByEmail[email];
                Console.WriteLine($"[CREATE] User with Email '{user.Email}' exists in Users but not in UserSnapshot.");
                await _usersChangePublish.SendMessageAsync(user, "create");
            }

            // Detect deletes
            foreach (var email in snapshotByEmail.Keys.Except(usersByEmail.Keys))
            {
                var user = snapshotByEmail[email];
                Console.WriteLine($"[DELETE] User with Email '{user.Email}' exists in UserSnapshot but not in Users.");
                await _usersChangePublish.SendMessageAsync(user, "delete");
            }

            // Detect updates
            foreach (var email in snapshotByEmail.Keys.Intersect(usersByEmail.Keys))
            {
                var user = usersByEmail[email];
                var snapshot = snapshotByEmail[email];

                bool needsUpdate =
                    snapshot.Name != user.Name ||
                    snapshot.PhoneNumber != user.PhoneNumber ||
                    snapshot.Role != user.Role ||
                    snapshot.Password != user.Password;

                if (needsUpdate)
                {
                    Console.WriteLine($"[UPDATE] User with Email '{email}' has different data in Users and UserSnapshot.");
                    await _usersChangePublish.SendMessageAsync(user, "update");
                }
            }

            // Replace the snapshot with the current state
            await _database.DropCollectionAsync("UserSnapshot");
            if (usersDocs.Count > 0)
            {
                await snapshotCollection.InsertManyAsync(usersDocs);
            }

            var tcs = new TaskCompletionSource();
            using (cancellationToken.Register(() => tcs.SetResult()))
            {
                await tcs.Task;
            }
        }

    }
}