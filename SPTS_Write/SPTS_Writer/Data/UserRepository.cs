using MongoDB.Driver;
using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;

namespace SPTS_Writer.Data;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly IMongoCollection<User> Users;
    public UserRepository(MongoDbContext context) : base(context)
    {
        Users = context.Users;

    }

    // Create operation
    public async Task<bool> CreateUserAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        user.CreatedAt = DateTime.UtcNow; // Set creation timestamp
        await Users.InsertOneAsync(user);
        return true;
    }

    // Update operation
    public async Task<bool> UpdateUserAsync(Guid id, User updatedUser)
    {
        if (updatedUser == null) throw new ArgumentNullException(nameof(updatedUser));

        var update = Builders<User>.Update
            .Set(u => u.Name, updatedUser.Name)
            .Set(u => u.Email, updatedUser.Email)
            .Set(u => u.Role, updatedUser.Role)
            .Set(u => u.UpdatedAt, DateTime.UtcNow); // Set update timestamp

        var result = await Users.UpdateOneAsync(u => u.Id == id, update);
        return result.ModifiedCount > 0;
    }


    // Delete operation
    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var result = await Users.DeleteOneAsync(u => u.Id == id);
        return result.DeletedCount > 0;
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

}
