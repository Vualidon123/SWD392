﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SWD_Clone.Models
{
    public class Users
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }
        public int ID { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
    }
    public class loginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
