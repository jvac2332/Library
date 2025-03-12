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
public class Book : Holding
{   // Constructors fot this class
    public int CopyrightYear { get; set; }
    public string Author { get; set; }

    public Book(int id, string title, string description, int copyrightYear, string author)
        : base(id, title, description)
    {
     if (copyrightYear < 1900 || copyrightYear > 2025)
         throw new ArgumentOutOfRangeException("Copyright year must be between 1900-2025.");
     CopyrightYear = copyrightYear;
     Author = author;
    }
    public override string HoldingType() => "Book";

    public override string ToString()
    {
        return base.ToString() + $"\n{Author}\n{CopyrightYear}";
    }
}

// Periodical subclass
class Periodical : Holding
{
    public string Date { get; }

    public Periodical(int id, string title, string description, string date)
        : base(id, title, description)
    {
        Date = date;
    }
    
    public override string HoldingType() => "Periodical";

    public override string ToString()
    {
        return base.ToString() + $"\n{Date}";
    }
}

class Library
{
    private List<Holding> holdings = new List<Holding>();

    public void AddHolding(Holding holding)
    {
        holdings.Add(holding);
    }

    public bool CheckOut(int id)
    {
        var holding = holdings.Find(h => h.Id == id);
        if (holding != null && !holding.IsCheckedOut)
        {
          holding.CheckOut();
          return true;
        } 
        return false;
    }

    public bool CheckIn(int id)
    {
        var holding = holdings.Find(h => h.Id == id);
        if (holding != null && holding.IsCheckedOut)
        {
            holding.CheckIn();
            return true;
        }
        return false;
    }

    public void ListAll()
    {
        Console.WriteLine("These holdings are currently available.");
        foreach (var h in holdings.FindAll(h => h.IsCheckedOut))
            Console.WriteLine(h);
        
        Console.WriteLine("\nThese holdings are currently available.");
        foreach(var h in holdings.FindAll(h => !h.IsCheckedOut))
            Console.WriteLine(h);
    }

    public void GetStats()
    {
        int checkedOut = holdings.FindAll(h => h.IsCheckedOut).Count;
        int available = holdings.Count - checkedOut;
        Console.WriteLine($"Available books: {available}\nChecked out books: {checkedOut}");
    }
}

// MAIN PROGRAM
class program
{
    public static void Main()
    {
        Library library = new Library();
        bool running = true;
        
        Console.WriteLine("*******************************************************");
        Console.WriteLine("         LIBRARY MANAGEMENT SYSTEM VERSION 1.0");
        Console.WriteLine("*******************************************************");

        while (running)
        {
            Console.WriteLine("Here are your choices: ");
            Console.WriteLine("1.List all holdings");
            Console.WriteLine("2.Add all books");
            Console.WriteLine("3.Add a periodical");
            Console.WriteLine("4.Place a holding");
            Console.WriteLine("5.Return a book");
            Console.WriteLine("6.see statistics");
            Console.WriteLine("Quit");
            Console.Write("Enter your choice: ");
            
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    library.ListAll();
                    break;
                case 2:
                    Console.Write("Enter ID Number: ");
                    int bookId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Title: ");
                    string bookTitle = Console.ReadLine();
                    Console.Write("Enter Description: ");
                    string bookDesc = Console.ReadLine();
                    Console.Write("Enter Copyright Year: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.Write("Enter Author: ");
                    string author = Console.ReadLine();
                    library.AddHolding(new Book(bookId, bookTitle, bookDesc, year, author));
                    break;
                case 3:
                    Console.Write("Enter ID Number: ");
                    int periodicalId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Title: ");
                    string periodicalTitle = Console.ReadLine();
                    Console.Write("Enter Description: ");
                    string periodicalDesc = Console.ReadLine();
                    Console.Write("Enter Date: ");
                    string date = Console.ReadLine();
                    library.AddHolding(new Periodical(periodicalId, periodicalTitle, periodicalDesc, date));
                    break;
                case 4:
                    Console.Write("Enter the ID Number of the holding to reserve: ");
                    int reserveId = int.Parse(Console.ReadLine());
                    Console.WriteLine(library.CheckOut(reserveId) ? "You have checked it out." : "There was a problem with your request.");
                    break;
                case 5:
                    Console.Write("Enter the ID Number of the holding to check in: ");
                    int returnId = int.Parse(Console.ReadLine());
                    Console.WriteLine(library.CheckIn(returnId) ? "You have checked it in." : "There was a problem with your request.");
                    break;
                case 6:
                    library.GetStats();
                    break;
                case 7:
                    running = false;
                    Console.WriteLine("Thank you for using this program.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
        
    }
}

