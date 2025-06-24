using SPTS_Writer.Data;
using SPTS_Writer.Data.Abstraction;
using SPTS_Writer.Entities;
using SPTS_Writer.Services.Abstraction;

namespace SPTS_Writer.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryService(MongoDbContext context)
        {
            _historyRepository = new HistoryRepository(context);
        }

        public async Task<History?> GetHistoryByIdAsync(string id)
        {
            return await _historyRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<History>> GetAllHistorysAsync()
        {
            return await _historyRepository.GetAllAsync();
        }

        public async Task AddHistoryAsync(History test)
        {
            await _historyRepository.AddAsync(test);
            await _historyRepository.SaveChangesAsync();
        }

        public async Task UpdateHistoryAsync(History test)
        {
            await _historyRepository.UpdateAsync(test.Id.ToString(), test);
            await _historyRepository.SaveChangesAsync();
        }

        public async Task DeleteHistoryAsync(string id)
        {
            await _historyRepository.DeleteAsync(id);
            await _historyRepository.SaveChangesAsync();
        }

        //INFP is for tiebreaker default if the couple have the same amount each
        // Pair	Tiebreaker default
        // E/I	I (people tend to underreport extraversion)
        // S/N	N (slight intuitive bias among MBTI fans)
        // T/F	F (more socially acceptable answer)
        // J/P	P (more common among younger users)
        private string GetMBTIResult(int[] answer)
        {
            string result = "";
            if (result[(int)MBTIAnswer.E] > result[(int)MBTIAnswer.I])
                result += MBTIAnswer.E.ToString();
            else result += MBTIAnswer.I.ToString();
            if (result[(int)MBTIAnswer.S] > result[(int)MBTIAnswer.N])
                result += MBTIAnswer.S.ToString();
            else result += MBTIAnswer.N.ToString();
            if (result[(int)MBTIAnswer.T] > result[(int)MBTIAnswer.F])
                result += MBTIAnswer.T.ToString();
            else result += MBTIAnswer.F.ToString();
            if (result[(int)MBTIAnswer.J] > result[(int)MBTIAnswer.P])
                result += MBTIAnswer.J.ToString();
            else result += MBTIAnswer.P.ToString();
            // if (result[0] > result[1])
            //     result += "E";
            // else result += "I";
            // if (result[2] > result[3])
            //     result += "S";
            // else result += "N";
            // if (result[4] > result[5])
            //     result += "T";
            // else result += "F";
            // if (result[6] > result[7])
            //     result += "J";
            // else result += "P";
            return result;
        }

        private string GetDISCResult(int[] answer)
        {
            int maxIndex = 0;
            for (int i = 1; i < answer.Length; ++i)
            {
                if (answer[maxIndex] < answer[i])
                {
                    maxIndex = i;
                }
            }
            maxIndex += 8;
            switch (maxIndex)
            {
                case (int)DISCAnswer.Dominance:
                    return DISCAnswer.Dominance.ToString();
                case (int)DISCAnswer.Influence:
                    return DISCAnswer.Influence.ToString();
                case (int)DISCAnswer.Conscientiousness:
                    return DISCAnswer.Conscientiousness.ToString();
                case (int)DISCAnswer.Steadiness:
                    return DISCAnswer.Steadiness.ToString();
                default: throw new Exception(maxIndex + " is out of range for disc answer ");
            }
        }

        private int[] EnumAnswerToArray<TEnum>(List<Answer> answers) where TEnum : Enum
        {
            int[] answerSheet = new int[Enum.GetValues(typeof(TEnum)).Length];
            foreach (Answer answer in answers)
            {
                bool match = false;
                int answerIndex = 0;
                foreach (string testAnswer in Enum.GetNames(typeof(TEnum)))
                {
                    if (answer.Value == testAnswer)
                    {
                        match = true;
                        ++answerSheet[(answerIndex)];
                        break;
                    }
                }
                if (match == false)
                {
                    throw new Exception(answer.Value + " is not a valid answer for " + nameof(TEnum) + " test method");
                }
            }
            return answerSheet;
        }

        private string GetTestFinalResult(TestMethod testType, List<Answer> answers)
        {
            int[] answerSheet;
            switch (testType)
            {
                case TestMethod.MBTI:
                    answerSheet = EnumAnswerToArray<MBTIAnswer>(answers);
                    return GetMBTIResult(answerSheet);
                case TestMethod.DISC:
                    answerSheet = EnumAnswerToArray<DISCAnswer>(answers);
                    return GetDISCResult(answerSheet);
                default:
                    throw new Exception(testType + " is not yet reconizable as a test method");
            }
        }

        public async Task<History> RecordTakenTestAsync(Test test, User who, List<Answer> answers)
        {
            History history = new History()
            {
                Id = Guid.NewGuid(),
                Answer = answers,
                Result = GetTestFinalResult(test.Method, answers),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                status = TestStatus.Completed,
                TestId = test.Id,
                UserId = who.Id,
            };
            return await _historyRepository.CreateHistoryAsync(history);
        }
    }
}

