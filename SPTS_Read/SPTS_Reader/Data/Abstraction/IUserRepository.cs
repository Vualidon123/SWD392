using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;

namespace SPTS_Reader.Data.Abstraction
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);

        Task<bool> UpdateRoleAsync(Guid id, string newRole);

        Task<long> CountAsync();
    }
}