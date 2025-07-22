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

    public async Task AddQuestionAsync(Question question)
    {
        // I know this is a rather bad fix but like, int is large enough to pass demo :3
        question.Id = (int)await _questionRepository.CountAsync();
        await _questionRepository.AddAsync(question);
        await _questionRepository.SaveChangesAsync();
    }

    public async Task UpdateQuestionAsync(Question question)
    {
        await _questionRepository.UpdateAsync(question.Id.ToString(), question);
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
