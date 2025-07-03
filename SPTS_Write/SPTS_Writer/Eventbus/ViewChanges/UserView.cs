using MongoDB.Bson;
using MongoDB.Driver;
using SPTS_Writer.Entities;

namespace SPTS_Writer.Eventbus.ViewChanges
{
    public class UserView
    {
        private readonly IMongoDatabase _database;

        public UserView(IMongoDatabase database)
        {
            _database = database;
        }

        // Example method to get the Users collection
        public IMongoCollection<User> GetUsersCollection()
        {
            return _database.GetCollection<User>("Users");
        }
        public async Task CreateAllTestsViewAsync()
        {
            try
            {
                var pipeline = new BsonDocument[0]; // Empty pipeline to include all documents and fields
                await _database.CreateViewAsync<BsonDocument, BsonDocument>("UserView", "tests", pipeline);
                Console.WriteLine("View 'allTestsView' created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating view: {ex.Message}");
            }
        }

        public async Task SyncUserViewWithUsersByRecreateAsync()
        {
            await CreateAllTestsViewAsync();
            var userViewCollection = _database.GetCollection<BsonDocument>("UserView");
            var usersCollection = _database.GetCollection<User>("Users");

            var userViewDocs = await userViewCollection.Find(FilterDefinition<BsonDocument>.Empty).ToListAsync();
            var usersDocs = await usersCollection.Find(FilterDefinition<User>.Empty).ToListAsync();

            var userViewByEmail = userViewDocs
                .Where(d => d.Contains("Email") && d["Email"].IsString)
                .ToDictionary(d => d["Email"].AsString.ToLowerInvariant(), d => d);

            var usersByEmail = usersDocs
                .Where(u => !string.IsNullOrEmpty(u.Email))
                .ToDictionary(u => u.Email.ToLowerInvariant(), u => u);

            // Detect creates
            var toCreate = usersByEmail.Keys.Except(userViewByEmail.Keys).ToList();
            foreach (var email in toCreate)
            {
                var user = usersByEmail[email];
                Console.WriteLine($"[CREATE] User with Email '{user.Email}' exists in Users but not in UserView.");
            }

            // Detect deletes
            var toDelete = userViewByEmail.Keys.Except(usersByEmail.Keys).ToList();
            foreach (var email in toDelete)
            {
                Console.WriteLine($"[DELETE] User with Email '{email}' exists in UserView but not in Users.");
            }

            // Detect updates
            var toUpdate = userViewByEmail.Keys.Intersect(usersByEmail.Keys).ToList();
            foreach (var email in toUpdate)
            {
                var user = usersByEmail[email];
                var viewDoc = userViewByEmail[email];

                bool needsUpdate =
                    viewDoc.GetValue("Name", "") != user.Name ||
                    viewDoc.GetValue("PhoneNumber", "") != user.PhoneNumber ||
                    viewDoc.GetValue("Role", "") != user.Role ||
                    viewDoc.GetValue("Password", "") != user.Password;

                if (needsUpdate)
                {
                    Console.WriteLine($"[UPDATE] User with Email '{email}' has different data in Users and UserView.");
                }
            }

            // Drop and recreate the view
            await _database.DropCollectionAsync("UserView");

            // Create a pipeline to project all fields from Users
            var pipeline = new[]
            {
        new BsonDocument("$project", new BsonDocument
        {
            { "Email", 1 },
            { "Name", 1 },
            { "PhoneNumber", 1 },
            { "Role", 1 },
            { "Password", 1 }
        })
    };

            await _database.CreateViewAsync<BsonDocument, BsonDocument>(
                "UserView",
                "Users",
                pipeline
            );

            Console.WriteLine("UserView has been dropped and recreated to sync with Users collection.");
        }



    }

}
