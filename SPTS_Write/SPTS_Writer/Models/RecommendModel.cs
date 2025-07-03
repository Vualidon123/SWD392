using SPTS_Writer.Entities;

namespace SPTS_Writer.Models
{
    public class RecommendModel
    {
        public string Specialization { get; set; }
        public List<School> Schools { get; set; } = new List<School>();
    }
}
