using SPTS_Writer.Data.Abstraction;

using SPTS_Writer.Entities;

namespace SPTS_Writer.Data;

public class QuestionRepository : Repository<Question>, IQuestionRepository
{
    public QuestionRepository(MongoDbContext context) : base(context) { }

    public async Task<Question> CreateQuestionAsync(Question newQuestion)
    {
        await base.AddAsync(newQuestion);
        await base.SaveChangesAsync();
        return newQuestion;
    }
}

