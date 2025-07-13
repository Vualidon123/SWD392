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
            if (answer[(int)MBTIAnswer.E] > answer[(int)MBTIAnswer.I])
                result += MBTIAnswer.E.ToString();
            else result += MBTIAnswer.I.ToString();
            if (answer[(int)MBTIAnswer.S] > answer[(int)MBTIAnswer.N])
                result += MBTIAnswer.S.ToString();
            else result += MBTIAnswer.N.ToString();
            if (answer[(int)MBTIAnswer.T] > answer[(int)MBTIAnswer.F])
                result += MBTIAnswer.T.ToString();
            else result += MBTIAnswer.F.ToString();
            if (answer[(int)MBTIAnswer.J] > answer[(int)MBTIAnswer.P])
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
                foreach (int testAnswer in Enum.GetValues(typeof(TEnum)))
                {
                    if (answer.Value == testAnswer.ToString())
                    {
                        match = true;
                        ++answerSheet[(answerIndex)];
                        break;
                    }
                    ++answerIndex;
                }
                if (match == false)
                {
                    throw new Exception(answer.Value + " is not a valid answer for " + typeof(TEnum).Name + " test method");
                }
            }
            return answerSheet;
        }

        private void DebugAnswerSheet(int[] answersheet)
        {
            int no = 0;
            foreach (int i in answersheet)
            {
                Console.WriteLine("Answer " + no + ": " + i);
                ++no;
            }
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

        public async Task<History> RecordTakenTestAsync(Test test, User who, List<Answer> answers, TestStatus status = TestStatus.InProgress)
        {
            History? target = await GetTakenTestAsync(test.Id, who.Id);
            History history = new History()
            {
                Id = target == null ? Guid.NewGuid() : target.Id,
                Answer = answers,
                Result = status == TestStatus.Completed ? GetTestFinalResult(test.Method, answers) : "",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                status = status,
                TestId = test.Id,
                UserId = who.Id,
            };
            if (target == null)
                return await _historyRepository.CreateHistoryAsync(history);
            else return await _historyRepository.UpdateHistoryAsync(target.Id, history);
        }

        public async Task<History?> GetTakenTestAsync(Guid testId, Guid userID)
        {
            return await _historyRepository.GetByTestIdAndUserIdAsync(testId, userID);
        }
    }
}

