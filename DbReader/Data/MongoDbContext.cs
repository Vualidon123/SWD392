
using System.Data;
using System;
using MongoDB.Bson;
using MongoDB.Driver;
using DbReader.Models;


public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<Test> Chats => _database.GetCollection<Test>("Tests");
    public IMongoCollection<History> Histories => _database.GetCollection<History>("Histories");
    public IMongoCollection<School> Schools => _database.GetCollection<School>("Schools");

    public IMongoCollection<T> GetCollection<T>()
    {
        if (typeof(T) == typeof(User))
            return (IMongoCollection<T>)Users;

        throw new ArgumentException("Collection not found for the given type");
    }

    // NO, NOT HERE
    public void SeedData()
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
    }
}
