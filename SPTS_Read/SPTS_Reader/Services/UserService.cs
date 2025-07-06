using SPTS_Reader.Data;
using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;
using SPTS_Reader.Services.Abstraction;

namespace SPTS_Reader.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(MongoDbContext context)
        {
            _userRepository = new UserRepository(context);
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task AddUserAsync(User test)
        {
            await _userRepository.AddAsync(test);
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User test)
        {
            await _userRepository.UpdateAsync(test.Id.ToString(), test);
            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateRoleAsync(Guid id, string newRole)
        {
            return await _userRepository.UpdateRoleAsync(id, newRole);
        }

        public async Task<long> CountAsync()
        {
            return await _userRepository.CountAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
        {
            // 🟢 Lấy danh sách User theo Role từ repository
            return await _userRepository.GetUsersByRoleAsync(role);
        }


    }
}
