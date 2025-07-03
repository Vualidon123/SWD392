﻿using MongoDB.Driver;
using SPTS_Writer.Entities;


public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
        // NukeData();
        SeedData();
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<Test> Tests => _database.GetCollection<Test>("Tests");
    public IMongoCollection<School> Schools => _database.GetCollection<School>("Schools");
    public IMongoCollection<History> Histories => _database.GetCollection<History>("Histories");
    public IMongoCollection<Question> Questions => _database.GetCollection<Question>("Questions");
    public IMongoCollection<SpecializationsRecommendation> SpecializationsRecommendations => _database.GetCollection<SpecializationsRecommendation>("SpecializationsRecommendations");

    public IMongoCollection<SystemLog> Logs => _database.GetCollection<SystemLog>("Logs");

    public IMongoCollection<Notification> Notifications => _database.GetCollection<Notification>("Notifications");



    public IMongoCollection<T> GetCollection<T>()
    {
        if (typeof(T) == typeof(User))
            return (IMongoCollection<T>)Users;
        if (typeof(T) == typeof(Test))
            return (IMongoCollection<T>)Tests;
        if (typeof(T) == typeof(School))
            return (IMongoCollection<T>)Schools;
        if (typeof(T) == typeof(History))
            return (IMongoCollection<T>)Histories;
        if (typeof(T) == typeof(Question))
            return (IMongoCollection<T>)Questions;
        if (typeof(T) == typeof(SpecializationsRecommendation))
            return (IMongoCollection<T>)SpecializationsRecommendations;
        throw new ArgumentException("Collection not found for the given type");
    }

    private void NukeData()
    {
        Users.DeleteMany(_ => true);
        Schools.DeleteMany(_ => true);
        Histories.DeleteMany(_ => true);
        Questions.DeleteMany(_ => true);
    }

    private void SeedData()
    {
        // Seed Emp data
        if (!Users.Find(_ => true).Any())
        {
            Users.InsertMany(new[]
                {
                    new User
                    {
                        Id= Guid.NewGuid(),
                        Name = "John Doe",
                        Email = "john@example.com",
                        PhoneNumber = "123-456-7890",
                        Password = "hashed_password_1" // In production, ensure passwords are properly hashed
                    },
                    new User
                    {
                        Id= Guid.NewGuid(),
                        Name = "Jane Smith",
                        Email = "jane@example.com",
                        PhoneNumber = "123-456-7891",
                        Password = "hashed_password_2"
                    },
                    new User
                    {
                        Id= Guid.NewGuid(),
                        Name = "Bob Wilson",
                        Email = "bob@example.com",
                        PhoneNumber = "123-456-7892",
                        Password = "hashed_password_3"
                    }
            });
        }
        List<Question> questions = SPTS_Writer.Utils.DataGenerator.GenerateSampleQuestions();
        if (!Questions.Find(_ => true).Any())
        {
            Questions.InsertMany(questions);
        }
        if (!Tests.Find(_ => true).Any())
        {
            Tests.InsertMany(SPTS_Writer.Utils.DataGenerator.GenerateSampleTests(questions));
        }
        // Seed Test data
    }
}
