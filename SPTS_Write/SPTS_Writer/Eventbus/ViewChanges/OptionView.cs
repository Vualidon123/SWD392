using MongoDB.Bson;
using MongoDB.Driver;
using SPTS_Writer.Entities;
using SPTS_Writer.Eventbus.Publishers;

namespace SPTS_Writer.Eventbus.ViewChanges
{
    public class OptionView 
    {

            private readonly IMongoDatabase _database;
            private readonly OptionChangePublish _questionChangePublish;

            public OptionView(IMongoDatabase database, OptionChangePublish questionChangePublish)
            {
                _database = database;
                _questionChangePublish = questionChangePublish;
            }
            public async Task CreateAllTestsViewAsync()
            {
                try
                {
                    var pipeline = new BsonDocument[0]; // Empty pipeline to include all documents and fields
                    await _database.CreateViewAsync<BsonDocument, BsonDocument>("OptionView", "option", pipeline);
                    Console.WriteLine("View created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating view: {ex.Message}");
                }
            }

            public async Task SyncTestSnapshotWithTestsAsync(CancellationToken cancellationToken)
            {
                var snapshotCollection = _database.GetCollection<Option>("OptionView");
                if (snapshotCollection == null)
                {
                    await CreateAllTestsViewAsync();
                }
                var testsCollection = _database.GetCollection<Option>("Options");

                var snapshotDocs = await snapshotCollection.Find(FilterDefinition<Option>.Empty).ToListAsync();
                var testsDocs = await testsCollection.Find(FilterDefinition<Option>.Empty).ToListAsync();

                var snapshotById = snapshotDocs
                    .Where(t => t.Id != null)
                    .ToDictionary(t => t.Id, t => t);

                var testsById = testsDocs
                    .Where(t => t.Id != null)
                    .ToDictionary(t => t.Id, t => t);

                // Detect creates
                foreach (var id in testsById.Keys.Except(snapshotById.Keys))
                {
                    var question = testsById[id];
                    Console.WriteLine($"[CREATE] Test with Id '{question.Id}' exists in Question but not in QuestionView.");
                    await _questionChangePublish.SendMessageAsync(question, "create");
                }

                // Detect deletes
                foreach (var id in snapshotById.Keys.Except(testsById.Keys))
                {
                    var question = snapshotById[id];
                    Console.WriteLine($"[DELETE] Question with Id '{question.Id}' exists in QuestionView but not in Question.");
                    await _questionChangePublish.SendMessageAsync(question, "delete");
                }

                // Detect updates
                foreach (var id in snapshotById.Keys.Intersect(testsById.Keys))
                {
                    var test = testsById[id];
                    var snapshot = snapshotById[id];

                    // Compare relevant fields for update detection
                    bool needsUpdate =
                        snapshot.Detail != test.Detail ||
                        snapshot.Value != test.Value 
                        ;

                    // Add more fields as needed

                    if (needsUpdate)
                    {
                        Console.WriteLine($"[UPDATE] Question with Id '{id}' has different data in Question and QuestionView.");
                        await _questionChangePublish.SendMessageAsync(test, "update");
                    }
                }

                // Replace the snapshot with the current state
                await _database.DropCollectionAsync("QuestionView");
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



