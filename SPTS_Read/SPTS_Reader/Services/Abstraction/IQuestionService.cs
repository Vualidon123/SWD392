using SPTS_Reader.Entities;

namespace SPTS_Reader.Services.Abstraction;
public interface IQuestionService
{
    public Task<Question?> GetQuestionByIdAsync(string id);
    public Task<IEnumerable<Question>> GetAllQuestionsAsync();
    public Task AddQuestionAsync(Question test);
    public Task UpdateQuestionAsync(Question test);

    Task<long> CountAsync();

}

