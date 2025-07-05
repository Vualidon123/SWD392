using MongoDB.Bson;
using MongoDB.Driver;
using SPTS_Writer.Entities;
using SPTS_Writer.Eventbus.Publishers;

namespace SPTS_Writer.Eventbus.ViewChanges
{
    public class SchoolView
    {
        private readonly IMongoDatabase _database;
        private readonly SchoolChangePublish _schoolChangePublish;

        public SchoolView(IMongoDatabase database, SchoolChangePublish schoolChangePublish)
        {
            _database = database;
            _schoolChangePublish = schoolChangePublish;
        }
        public async Task CreateAllTestsViewAsync()
        {
            try
            {
                var pipeline = new BsonDocument[0]; // Empty pipeline to include all documents and fields
                await _database.CreateViewAsync<BsonDocument, BsonDocument>("SchoolView", "", pipeline);
                Console.WriteLine("View 'allTestsView' created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating view: {ex.Message}");
            }
        }

        public async Task SyncTestSnapshotWithTestsAsync(CancellationToken cancellationToken)
        {
            var snapshotCollection = _database.GetCollection<School>("SchoolView");
            if (snapshotCollection == null)
            {
                await CreateAllTestsViewAsync();
            }
            var testsCollection = _database.GetCollection<School>("Schools");

            var snapshotDocs = await snapshotCollection.Find(FilterDefinition<School>.Empty).ToListAsync();
            var testsDocs = await testsCollection.Find(FilterDefinition<School>.Empty).ToListAsync();

            var snapshotById = snapshotDocs
                .Where(t => t.Id != Guid.Empty)
                .ToDictionary(t => t.Id, t => t);

            var testsById = testsDocs
                .Where(t => t.Id != Guid.Empty)
                .ToDictionary(t => t.Id, t => t);

            // Detect creates
            foreach (var id in testsById.Keys.Except(snapshotById.Keys))
            {
                var test = testsById[id];
                Console.WriteLine($"[CREATE] Test with Id '{test.Id}' exists in Schools but not in SchoolView.");
                await _schoolChangePublish.SendMessageAsync(test, "create");
            }

            // Detect deletes
            foreach (var id in snapshotById.Keys.Except(testsById.Keys))
            {
                var test = snapshotById[id];
                Console.WriteLine($"[DELETE] School with Id '{test.Id}' exists in SchoolView but not in Schools.");
                await _schoolChangePublish.SendMessageAsync(test, "delete");
            }

            // Detect updates
            foreach (var id in snapshotById.Keys.Intersect(testsById.Keys))
            {
                var test = testsById[id];
                var snapshot = snapshotById[id];

                // Compare relevant fields for update detection
                bool needsUpdate =
                    snapshot.Specializations != test.Specializations ||
                    snapshot.Name != test.Name ||
                    snapshot.Ranking != test.Ranking 
                    ;

                // Add more fields as needed

                if (needsUpdate)
                {
                    Console.WriteLine($"[UPDATE] School with Id '{id}' has different data in Schools and SchoolView.");
                    await _schoolChangePublish.SendMessageAsync(test, "update");
                }
            }

            // Replace the snapshot with the current state
            await _database.DropCollectionAsync("SchoolView");
            if (testsDocs.Count > 0)
            {
                await snapshotCollection.InsertManyAsync(testsDocs);
            }

            var tcs = new TaskCompletionSource();
            using (cancellationToken.Register(() => tcs.SetResult()))
            {
                await tcs.Task;
            }
        }
    }
}

