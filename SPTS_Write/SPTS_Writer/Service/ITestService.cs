using SPTS_Writer.Models;

namespace SPTS_Writer.Service
{
    public interface ITestService
    {
        public  Task<Test?> GetTestByIdAsync(string id);

        public  Task<IEnumerable<Test>> GetAllTestsAsync();

        public  Task AddTestAsync(Test test);



        public  Task UpdateTestAsync(Test test);


        public  Task DeleteTestAsync(string id);
        

    }
}
