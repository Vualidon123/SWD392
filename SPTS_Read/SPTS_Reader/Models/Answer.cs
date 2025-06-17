namespace SPTS_Reader.Models
{
    public class Answer
    {
        public string Value { get; set; }
        public Guid QuestionId { get; set; }
        public Guid AnswerId { get; set; } 
    }
}