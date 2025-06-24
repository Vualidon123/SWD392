using SPTS_Writer.Entities;

namespace SPTS_Writer.Data.Abstraction;
public interface IQuestionRepository : IRepository<Question>
{
    Task<Question> CreateQuestionAsync(Question question);
}


