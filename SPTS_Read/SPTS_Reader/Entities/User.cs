﻿namespace SPTS_Reader.Entities;

public class User : Base
{
    public string PhoneNumber { get; set; }
    public string Role { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}