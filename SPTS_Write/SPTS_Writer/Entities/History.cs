namespace SPTS_Writer.Entities;

public class History : Base
{
    public Guid UserId { get; set; }
    public Guid TestId { get; set; }
    public string Result { get; set; }
    public TestStatus status { get; set; }
    public List<Answer> Answer { get; set; }
}

public enum TestStatus
{
    InProgress = 1,
    Completed = 2,
    Discard = 3, // ? instead of Discarded we delete the whole history?
}

public class TestSubmission
{
    public string? TestID { get; set; }
    public string? WhomID { get; set; }
    public List<Answer> answers { get; set; } = null!;
}
