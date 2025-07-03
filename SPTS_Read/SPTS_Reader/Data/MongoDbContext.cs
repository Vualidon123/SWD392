using System.Data;
using System;
using MongoDB.Bson;
using MongoDB.Driver;
using SPTS_Reader.Entities;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<Test> Tests => _database.GetCollection<Test>("Tests");
    public IMongoCollection<History> Histories => _database.GetCollection<History>("Histories");
    public IMongoCollection<School> Schools => _database.GetCollection<School>("Schools");
    public IMongoCollection<SpecializationsRecommendation> SpecializationsRecommendations => _database.GetCollection<SpecializationsRecommendation>("SpecializationsRecommendations");

    public IMongoCollection<Question> Questions => _database.GetCollection<Question>("Questions");

    public IMongoCollection<T> GetCollection<T>()
    {
        if (typeof(T) == typeof(Question))
            return (IMongoCollection<T>)Questions;
        if (typeof(T) == typeof(User))
            return (IMongoCollection<T>)Users;
        if (typeof(T) == typeof(Test))
            return (IMongoCollection<T>)Tests;
        if (typeof(T) == typeof(SpecializationsRecommendation))
            return (IMongoCollection<T>)SpecializationsRecommendations;
        throw new ArgumentException("Collection not found for the given type");
    }
}
