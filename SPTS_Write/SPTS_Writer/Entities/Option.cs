﻿namespace SPTS_Writer.Entities;

public class Option
{
    public int Id { get; set; }
    public string Detail { get; set; }
    public AllAnswer Value { get; set; } // e.g., "M|B|T", "D|T|B|I" 
}
