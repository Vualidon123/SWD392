namespace SPTS_Writer.Entities
{
    public class SpecializationsRecommendation: Base
    {
        public string SpecializationName { get; set; }
        public List<string> RecommendPersonality { get; set; } = new List<string>();
    }
}
