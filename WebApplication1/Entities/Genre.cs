using System;

namespace WebApplication1.Entities;

public class Genre
{
    public int Id { get; set; }
    //public string Name { get; set; } = string.Empty;

    public required string Name { get; set; }
}
