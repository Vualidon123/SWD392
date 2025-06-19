using MongoDB.Driver;
using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;

namespace SPTS_Writer.Data;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(MongoDbContext context) : base(context) { }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await FindAsync(u => u.Email == email);
    }
}