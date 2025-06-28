namespace SPTS_Reader.Entities;

public class Question
{
    public int Id { get; set; }
    public string Detail { get; set; }
    public string Type { get; set; } // e.g., "M/B/T", "D/T/B/I"
    public List<Option> Options { get; set; }
}