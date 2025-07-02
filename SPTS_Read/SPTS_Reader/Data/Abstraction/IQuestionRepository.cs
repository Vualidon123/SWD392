using SPTS_Reader.Data.Abstraction;
using SPTS_Reader.Entities;

namespace SPTS_Reader.Data.Abstraction;
public interface IQuestionRepository : IRepository<Question>
{
    Task<Question> CreateQuestionAsync(Question question);

    Task<long> CountAsync();

}


