namespace SPTS_Writer.Models
{
    public class Question : Base
    {
        public string Detail { get; set; }
        public string Type { get; set; } // e.g., "M/B/T", "D/T/B/I"
        public List<Option> Options { get; set; }
    }
}