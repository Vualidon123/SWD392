using SPTS_Reader.Entities;

namespace SPTS_Reader.Services.Abstraction;
public interface ITestService
{
    Task<List<Test>> GetByAuthorIdAsync(string authorId);
    Task<List<Test>> GetByTestMethodAsync(TestMethod method, int limit, int skip);
    Task<List<Test>> GetBatchAsync(int limit, int skip);
    Task<Test?> GetByIdAsync(Guid id);
    Task<long> CountAsync();
    Task<Test?> GetTestByIdAsync(string id);

    Task<IEnumerable<Test>> GetAllTestsAsync();
    Task AddTestAsync(Test test);
    Task UpdateTestAsync(Test test);
    Task DeleteTestAsync(string id);
    /*Task<bool> UpdateTestMethodAsync(Guid id, TestMethod newMethod);
    Task<bool> UpdateAuthorAsync(Guid id, string newAuthor);
    Task<bool> UpdateTestDateAsync(Guid id, DateTime newDate);
    Task<bool> UpdateNumberOfQuestionsAsync(Guid id, int newNumberOfQuestions);*/

}

