using SPTS_Writer.Entities;

namespace SPTS_Writer.Data.Abstraction;
public interface IQuestionRepository : IRepository<Question>
{
    public Task<Question> CreateQuestionAsync(Question question);
    public Task<List<Question>> GetRandomQuestionsAsync(TestMethod method, int amount);
}
