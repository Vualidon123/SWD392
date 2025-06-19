using SPTS_Writer.Data;
using SPTS_Writer.Models;

namespace SPTS_Writer.Service
{
    public class TestService : ITestService
    {
        private readonly IRepository<Test> _testRepository;
        public TestService(IRepository<Test> testRepository)
        {
            _testRepository = testRepository;
        }
        public async Task<Test?> GetTestByIdAsync(string id)
        {
            return await _testRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Test>> GetAllTestsAsync()
        {
            return await _testRepository.GetAllAsync();
        }

        public async Task AddTestAsync(Test test)
        {
            await _testRepository.AddAsync(test);
            await _testRepository.SaveChangesAsync();
        }

        public async Task UpdateTestAsync(Test test)
        {
            await _testRepository.UpdateAsync(test.Id.ToString(), test);
            await _testRepository.SaveChangesAsync();
        }
        public async Task DeleteTestAsync(string id)
        {
            await _testRepository.DeleteAsync(id);
            await _testRepository.SaveChangesAsync();
        }
    }
}