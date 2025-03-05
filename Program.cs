// See https://aka.ms/new-console-template for more information

//Console.WriteLine("Hello, World!");

// Library
//Joshua Vachachira
// NetProgramming

using System;

// Base class to represent a library holdings
public abstract class Holding
{
    // Puttng the constructor in 
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCheckedOut { get; private set; }

    public Holding(int id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
        IsCheckedOut = false;
    }

    public void CheckOut()
    {
        IsCheckedOut = true;
    }

    public void CheckIn()
    {
        IsCheckedOut = false;
    }

    public abstract string HoldingType();

    public override string ToString()
    {
        return $"{Id} \n {HoldingType()}\n{Title}\n{Description}\n{(IsCheckedOut ? "Checked Out" : "Available")}";
    }
}

// Book subclass
public class Book : Holding // still figuring out the error
{
    public int CopyrightYear { get; set; }
    public string Author { get; set; }

    public Book(int id, string title, string description, int copyrightYear, string author)
        : base(id, title, description)
    {
        
    }
}

