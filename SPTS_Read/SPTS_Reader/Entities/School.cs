namespace SPTS_Reader.Entities;

public class School: Base
{
    public string Name { get; set; }
    public Specializations[] Specializations { get; set; }
}
