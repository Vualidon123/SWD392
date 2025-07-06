using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using SPTS_Writer.Entities;
using SPTS_Writer.Eventbus.Publishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPTS_Writer.Eventbus.ViewChanges
{
    public class TestView
    {
        private readonly IMongoDatabase _database;
        private readonly TestChangePublish _testsChangePublish;

        public TestView(IMongoDatabase database, TestChangePublish testsChangePublish)
        {
            _database = database;
            _testsChangePublish = testsChangePublish;
        }
        public async Task CreateAllTestsViewAsync()
        {
            try
            {
                var pipeline = new BsonDocument[0]; // Empty pipeline to include all documents and fields
                await _database.CreateViewAsync<BsonDocument, BsonDocument>("TestView", "test", pipeline);
                Console.WriteLine("View 'allTestsView' created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating view: {ex.Message}");
            }
        }

        public async Task SyncTestSnapshotWithTestsAsync(CancellationToken cancellationToken)
        {
            var snapshotCollection = _database.GetCollection<Test>("TestView");
            if (snapshotCollection == null)
            {
                await CreateAllTestsViewAsync();
            }
            var testsCollection = _database.GetCollection<Test>("Tests");

            var snapshotDocs = await snapshotCollection.Find(FilterDefinition<Test>.Empty).ToListAsync();
            var testsDocs = await testsCollection.Find(FilterDefinition<Test>.Empty).ToListAsync();

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
                Console.WriteLine($"[CREATE] Test with Id '{test.Id}' exists in Tests but not in TestSnapshot.");
                await _testsChangePublish.SendMessageAsync(test, "create");
            }

            // Detect deletes
            foreach (var id in snapshotById.Keys.Except(testsById.Keys))
            {
                var test = snapshotById[id];
                Console.WriteLine($"[DELETE] Test with Id '{test.Id}' exists in TestSnapshot but not in Tests.");
                await _testsChangePublish.SendMessageAsync(test, "delete");
            }

            // Detect updates
            foreach (var id in snapshotById.Keys.Intersect(testsById.Keys))
            {
                var test = testsById[id];
                var snapshot = snapshotById[id];

                // Compare relevant fields for update detection
                bool needsUpdate =
                    snapshot.Method != test.Method ||
                    snapshot.TestDate != test.TestDate ||
                    snapshot.Questions != test.Questions ||
                    snapshot.NumberOfQuestions != test.NumberOfQuestions ||
                    snapshot.Author != test.Author 
                    ;
                        
                // Add more fields as needed

                if (needsUpdate)
                {
                    Console.WriteLine($"[UPDATE] Test with Id '{id}' has different data in Tests and TestSnapshot.");
                    await _testsChangePublish.SendMessageAsync(test, "update");
                }
            }

            // Replace the snapshot with the current state
            await _database.DropCollectionAsync("TestSnapshot");
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
