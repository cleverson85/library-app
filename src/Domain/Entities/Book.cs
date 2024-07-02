﻿namespace Domain.Entities;

public class Book : BaseEntity
{
    public Book() { } 

    public Book(string author, string title, string registerNumber)
    {
        Author = author;
        Title = title;
        RegisterNumber = registerNumber;
    }

    public string Author { get; set; }
    public string Title { get; set; }
    public string RegisterNumber { get; set; }
}
