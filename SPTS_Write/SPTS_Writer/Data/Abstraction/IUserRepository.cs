using SPTS_Writer.Entities;

namespace SPTS_Writer.Data.Abstraction
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}