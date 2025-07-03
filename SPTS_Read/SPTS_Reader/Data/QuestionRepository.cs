using MongoDB.Driver;
using SPTS_Reader.Data;
using SPTS_Reader.Data.Abstraction;

using SPTS_Reader.Entities;

namespace SPTS_Reader.Data;

public class QuestionRepository : Repository<Question>, IQuestionRepository
{
    private readonly IMongoCollection<Question> _questions;

    public QuestionRepository(MongoDbContext context) : base(context)
    {
        _questions = context.Questions;
    }

    public async Task<Question> CreateQuestionAsync(Question newQuestion)
    {
        await base.AddAsync(newQuestion);
        await base.SaveChangesAsync();
        return newQuestion;
    }

    public async Task<long> CountAsync()
    {
        return await _questions.CountDocumentsAsync(_ => true);
    }


}

