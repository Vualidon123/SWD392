using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;
using SPTS_Writer.Services.Abstraction;

namespace SPTS_Writer.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;

        public TestService(ITestRepository testRepository, IQuestionRepository questionRepository)
        {
            _testRepository = testRepository;
            _questionRepository = questionRepository;
        }

        public async Task<Test?> GetTestByIdAsync(string id)
        {
            return await _testRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Test>> GetAllTestsAsync()
        {
            var tests = await _testRepository.GetAllAsync();
            tests = tests.Where(test => test.IsRandomized == false);
            return tests;
        }

        public async Task AddTestAsync(Test test)
        {
            test.Id = new Guid();
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

        public async Task<long> CountAsync()
        {
            return await _testRepository.CountAsync();
        }

        public async Task<Test> GenerateRandomTestAsync(TestMethod method, int amount, string userId)
        {
            List<Question> allQuestions = await _questionRepository.GetRandomQuestionsAsync(method, amount);
            var test = new Test()
            {
                Id = Guid.NewGuid(), // Assuming Base has Id
                Method = method,
                Author = $"{userId}",
                TestDate = DateTime.Now,
                NumberOfQuestions = allQuestions.Count(),
                Questions = allQuestions,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsRandomized = true,
            };
            await _testRepository.AddAsync(test);
            return test;
        }
    }
}
