using SPTS_Writer.Entities;

namespace SPTS_Writer.Services.Abstraction;
public interface IUserService
{
    public Task<User?> GetUserByIdAsync(string id);
    public Task<IEnumerable<User>> GetAllUsersAsync();
    public Task AddUserAsync(User user);
    public Task UpdateUserAsync(User user);

    Task DeleteUserAsync(string id);
    Task<bool> UpdateRoleAsync(Guid id, string newRole);

    Task<long> CountAsync();

}
