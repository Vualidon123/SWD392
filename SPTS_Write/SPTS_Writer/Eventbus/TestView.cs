using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using SPTS_Writer.Entities; // Import to use Test and TestMethod

namespace SPTS_Writer.Eventbus
{
    public class TestView
    {
        private readonly IMongoDatabase _database;

        public TestView()
        {
            string connectionString = "mongodb://localhost:27017"; // Replace with your connection string
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("SWDClone"); // Replace with your database name
        }

        // Create a view for MBTI tests
        public async Task CreateAllTestsViewAsync()
        {
            try
            {
                var pipeline = new BsonDocument[0]; // Empty pipeline to include all documents and fields

                
                await _database.CreateViewAsync<BsonDocument, BsonDocument>("allTestsView", "tests", pipeline);
                Console.WriteLine("View 'allTestsView' created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating view: {ex.Message}");
            }
        }

        // Query the view
        public async Task QueryAllTestsViewAsync()
        {
            try
            {
                var view = _database.GetCollection<Test>("Tests");
                var results = await view.Find(Builders<Test>.Filter.Empty).ToListAsync();

                foreach (var test in results)
                {
                    Console.WriteLine($"ID: {test.Id}, Method: {test.Method}, Author: {test.Author}, " +
                                      $"Test Date: {test.TestDate}, Questions: {test.NumberOfQuestions}, " +
                                      $"Question Count: {test.Questions?.Count ?? 0}, " +
                                      $"Created: {test.CreatedAt}, Updated: {test.UpdatedAt}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error querying view: {ex.Message}");
            }
        }

        // List all views
        public async Task ListViewsAsync()
        {
            try
            {
                var command = new BsonDocument { { "listCollections", 1 }, { "filter", new BsonDocument("type", "view") } };
                var result = await _database.RunCommandAsync<BsonDocument>(command);
                foreach (var collection in result["cursor"]["firstBatch"].AsBsonArray)
                {
                    Console.WriteLine($"View: {collection["name"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing views: {ex.Message}");
            }
        }

        // Update a specific test in the underlying collection and refresh the view
        public async Task UpdateTestInViewAsync(Guid testId, Test updatedTest)
        {
            try
            {
                var testsCollection = _database.GetCollection<Test>("Tests");
                
                // Update the test in the underlying collection
                updatedTest.UpdatedAt = DateTime.UtcNow;
                var filter = Builders<Test>.Filter.Eq(t => t.Id, testId);
                var update = Builders<Test>.Update
                    .Set(t => t.Method, updatedTest.Method)
                    .Set(t => t.Author, updatedTest.Author)
                    .Set(t => t.TestDate, updatedTest.TestDate)
                    .Set(t => t.NumberOfQuestions, updatedTest.NumberOfQuestions)
                    .Set(t => t.Questions, updatedTest.Questions)
                    .Set(t => t.UpdatedAt, updatedTest.UpdatedAt);

                var result = await testsCollection.UpdateOneAsync(filter, update);
                
                if (result.ModifiedCount > 0)
                {
                    Console.WriteLine($"Test with ID {testId} updated successfully in underlying collection.");
                    // Refresh the view by recreating it
                    await RefreshViewAsync();
                }
                else
                {
                    Console.WriteLine($"No test found with ID {testId} to update.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating test in view: {ex.Message}");
            }
        }

        // Refresh the view by dropping and recreating it
        public async Task RefreshViewAsync()
        {
            try
            {
                // Drop the existing view
                await _database.DropCollectionAsync("allTestsView");
                Console.WriteLine("Existing allTestsView dropped.");
                
                // Recreate the view
                await CreateAllTestsViewAsync();
                Console.WriteLine("allTestsView refreshed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing view: {ex.Message}");
            }
        }

        // Import List<Test> and create allTestsView from the imported data
        public async Task UpdateMultipleTestsInViewAsync(List<Test> testsToImport)
        {
            try
            {
                // Drop the existing view
                await _database.DropCollectionAsync("allTestsView");
                Console.WriteLine("Existing allTestsView dropped.");

                // Create a temporary collection for the imported data
                var tempCollectionName = "tempTestsForView";
                var tempCollection = _database.GetCollection<Test>(tempCollectionName);
                
                // Clear any existing temp collection
                await _database.DropCollectionAsync(tempCollectionName);
                
                // Insert the tests into temp collection
                if (testsToImport.Any())
                {
                    await tempCollection.InsertManyAsync(testsToImport);
                    Console.WriteLine($"Imported {testsToImport.Count} tests into temporary collection.");
                }

                // Create the view from the temp collection
                var pipeline = new BsonDocument[0]; // Empty pipeline to include all documents and fields
                await _database.CreateViewAsync<BsonDocument, BsonDocument>("allTestsView", tempCollectionName, pipeline);
                Console.WriteLine("allTestsView created from imported test data.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating view from imported tests: {ex.Message}");
            }
        }

        public async Task CheckAndUpdateViewAsync()
        {
            try
            {
                // Check if view exists
                var command = new BsonDocument { { "listCollections", 1 }, { "filter", new BsonDocument("name", "allTestsView") } };
                var result = await _database.RunCommandAsync<BsonDocument>(command);
                bool viewExists = result["cursor"]["firstBatch"].AsBsonArray.Any();

                if (!viewExists)
                {
                    Console.WriteLine("View 'allTestsView' does not exist. Creating view...");
                    await CreateAllTestsViewAsync();
                    return;
                }

                // Get documents from tests collection and allTestsView
                var testsCollection = _database.GetCollection<Test>("Tests");
                var view = _database.GetCollection<Test>("allTestsView");

                var testsDocs = await testsCollection.Find(Builders<Test>.Filter.Empty).ToListAsync();
                var viewDocs = await view.Find(Builders<Test>.Filter.Empty).ToListAsync();

                bool hasDifferences = false;
                List<Test> testsToUpdate = new List<Test>();

                // Compare document counts
                if (testsDocs.Count != viewDocs.Count)
                {
                    Console.WriteLine($"Difference detected: tests collection has {testsDocs.Count} documents, " +
                                      $"but allTestsView has {viewDocs.Count} documents.");
                    hasDifferences = true;
                }

                // Compare documents by _id and fields
                var testsDict = testsDocs.ToDictionary(t => t.Id, t => t);
                var viewDict = viewDocs.ToDictionary(t => t.Id, t => t);

                // Check for missing or extra documents
                foreach (var testId in testsDict.Keys)
                {
                    if (!viewDict.ContainsKey(testId))
                    {
                        Console.WriteLine($"Document with ID {testId} is in tests but missing in allTestsView.");
                        hasDifferences = true;
                        testsToUpdate.Add(testsDict[testId]);
                    }
                    else
                    {
                        // Compare fields
                        var testDoc = testsDict[testId];
                        var viewDoc = viewDict[testId];

                        if (!AreTestsEqual(testDoc, viewDoc))
                        {
                            Console.WriteLine($"Document with ID {testId} differs between tests and allTestsView.");
                            hasDifferences = true;
                            testsToUpdate.Add(testDoc);
                        }
                    }
                }

                foreach (var viewId in viewDict.Keys)
                {
                    if (!testsDict.ContainsKey(viewId))
                    {
                        Console.WriteLine($"Document with ID {viewId} is in allTestsView but missing in tests.");
                        hasDifferences = true;
                    }
                }

                // If differences found, update the view data
                if (hasDifferences)
                {
                    Console.WriteLine("Differences found. Updating allTestsView data...");
                    
                    if (testsToUpdate.Any())
                    {
                        Console.WriteLine($"Updating {testsToUpdate.Count} tests in the view...");
                        await UpdateMultipleTestsInViewAsync(testsToUpdate);
                    }
                    else
                    {
                        // If no specific tests to update, just refresh the view
                        await RefreshViewAsync();
                    }
                }
                else
                {
                    Console.WriteLine("No differences found between tests collection and allTestsView.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking/updating view: {ex.Message}");
            }
        }

        private bool AreTestsEqual(Test test1, Test test2)
        {
            if (test1.Id != test2.Id ||
                test1.CreatedAt != test2.CreatedAt ||
                test1.UpdatedAt != test2.UpdatedAt ||
                test1.Method != test2.Method ||
                test1.Author != test2.Author ||
                test1.TestDate != test2.TestDate ||
                test1.NumberOfQuestions != test2.NumberOfQuestions ||
                (test1.Questions?.Count ?? 0) != (test2.Questions?.Count ?? 0))
            {
                return false;
            }

            // Compare Questions array (basic comparison on Text property)
            if (test1.Questions != null && test2.Questions != null)
            {
                if (test1.Questions.Count != test2.Questions.Count)
                    return false;

                for (int i = 0; i < test1.Questions.Count; i++)
                {
                    if (test1.Questions[i].Detail != test2.Questions[i].Detail)
                        return false;
                }
            }
            else if (test1.Questions != test2.Questions) // One is null, other isn't
            {
                return false;
            }

            return true;
        }
    }
}