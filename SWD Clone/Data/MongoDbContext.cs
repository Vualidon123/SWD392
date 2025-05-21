
using System.Data;
using System;
using MongoDB.Bson;
using MongoDB.Driver;
using SWD_Clone.Models;


    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Users> user => _database.GetCollection<Users>("Users");

        public IMongoCollection<T> GetCollection<T>()
        {
            if (typeof(T) == typeof(Users))
                return (IMongoCollection<T>)user;

            throw new ArgumentException("Collection not found for the given type");
        }
        public void SeedData()
        {
            // Seed Emp data
            if (!user.Find(_ => true).Any())
            {
                var johnId = ObjectId.GenerateNewId();
                var janeId = ObjectId.GenerateNewId();
                var bobId = ObjectId.GenerateNewId();
                user.InsertMany(new[]
                {
            new Users
            {
                ObjectId= johnId,
                ID = 1,
                Name = "John Doe",
                Email = "john@example.com",
                PhoneNumber = "123-456-7890",
                Password = "hashed_password_1" // In production, ensure passwords are properly hashed
            },
            new Users
            {
                ObjectId= janeId,
                ID = 2,
                Name = "Jane Smith",
                Email = "jane@example.com",
                PhoneNumber = "123-456-7891",
                Password = "hashed_password_2"
            },
            new Users
            {
                ObjectId= bobId,
                ID = 3,
                Name = "Bob Wilson",
                Email = "bob@example.com",
                PhoneNumber = "123-456-7892",
                Password = "hashed_password_3"
            }
        });
            }



        }
    }
