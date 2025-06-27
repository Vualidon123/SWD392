namespace SPTS_Writer.Entities
{
    public class SystemLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Action { get; set; }
        public string PerformedBy { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

}
