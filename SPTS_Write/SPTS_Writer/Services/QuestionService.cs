using SPTS_Writer.Data;
using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;
using SPTS_Writer.Services.Abstraction;

namespace SPTS_Writer.Services;
public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(MongoDbContext context)
    {
        _questionRepository = new QuestionRepository(context);
    }

    public async Task<Question?> GetQuestionByIdAsync(string id)
    {
        return await _questionRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
    {
        return await _questionRepository.GetAllAsync();
    }

    public async Task AddQuestionAsync(Question test)
    {
        await _questionRepository.AddAsync(test);
        await _questionRepository.SaveChangesAsync();
    }

    public async Task UpdateQuestionAsync(Question test)
    {
        await _questionRepository.UpdateAsync(test.Id.ToString(), test);
        await _questionRepository.SaveChangesAsync();
    }

    public async Task DeleteQuestionAsync(string id)
    {
        await _questionRepository.DeleteAsync(id);
        await _questionRepository.SaveChangesAsync();
    }

    public async Task<long> CountAsync()
    {
        return await _questionRepository.CountAsync();
    }
}
