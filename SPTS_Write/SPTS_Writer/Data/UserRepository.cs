using MongoDB.Driver;
using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;

namespace SPTS_Writer.Data;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly IMongoCollection<User> Users;
    public UserRepository(MongoDbContext context) : base(context) {
        Users = context.Users;

    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await FindAsync(u => u.Email == email);
    }

    public async Task<bool> UpdateRoleAsync(Guid id, string newRole)
    {
        var update = Builders<User>.Update.Set(u => u.Role, newRole);
        var result = await Users.UpdateOneAsync(u => u.Id == id, update);
        return result.ModifiedCount > 0;
    }

    public async Task<long> CountAsync() => await Users.CountDocumentsAsync(_ => true);


}