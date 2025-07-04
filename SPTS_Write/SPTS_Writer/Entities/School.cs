namespace SPTS_Writer.Entities;

public class School: Base
{
    public string Name { get; set; }
    public Specializations[] Specializations { get; set; }
    public int Ranking { get; set; }
}
