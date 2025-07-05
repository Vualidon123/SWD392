using SPTS_Reader.Entities;

namespace SPTS_Reader.Models
{
    public class RecommendModel
    {
        public string Specialization { get; set; }
        public List<School> Schools { get; set; } = new List<School>();
    }
}
