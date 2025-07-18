namespace SPTS_Writer.Entities
{
    public class Notification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Message { get; set; }
        public Guid? UserId { get; set; }
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
