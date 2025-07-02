namespace SPTS_Reader.Entities;

public class Answer
{
    public string Value { get; set; }
    public int QuestionId { get; set; }
    public int AnswerId { get; set; }
}

enum MBTIAnswer
{
    E = 0,
    I = 1,
    S = 2,
    N = 3,
    T = 4,
    F = 5,
    J = 6,
    P = 7,
}

enum DISCAnswer
{
    Dominance = 8,
    Influence = 9,
    Steadiness = 10,
    Conscientiousness = 11,
}

public enum AllAnswer
{
    E = 0,
    I = 1,
    S = 2,
    N = 3,
    T = 4,
    F = 5,
    J = 6,
    P = 7,
    Dominance = 8,
    Influence = 9,
    Steadiness = 10,
    Conscientiousness = 11,
}

