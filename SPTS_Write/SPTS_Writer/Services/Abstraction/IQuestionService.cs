using SPTS_Writer.Entities;

namespace SPTS_Writer.Services.Abstraction;
public interface IQuestionService
{
    public Task<Question?> GetQuestionByIdAsync(string id);
    public Task<IEnumerable<Question>> GetAllQuestionsAsync();
    public Task AddQuestionAsync(Question test);
    public Task UpdateQuestionAsync(Question test);

    Task<long> CountAsync();

}

