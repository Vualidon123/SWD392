using MongoDB.Driver;
using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Data;
using SPTS_Reader.Entities;

namespace SPTS_Reader.Data;

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

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
    {
        return await Users
            .Find(u => u.Role == role)
            .ToListAsync();
    }


}