// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        /**
        ** Walk through the next few examples when covering What is a Data Structure and the Core Operations?
        **/

        // This is an array of characters, which is a fixed-size collection
        string myString = "Hello, World!";
        Console.WriteLine("Length of string: " + myString.Length);

        // Accessing characters by index (lookup)
        Console.WriteLine("Character at index 7: " + myString[7]);
        // Access a substring (search)
        Console.WriteLine("Substring from index 7: " + myString.Substring(7));
        // Finding the index of a character (search)
        Console.WriteLine("Index of 'World': " + myString.IndexOf("World"));

        // Inserting a character (insertion)
        string newString = myString.Insert(7, "Beautiful ");
        Console.WriteLine("After insertion: " + newString);

        // Removing a character (deletion)
        string removedString = newString.Remove(7, 10);
        Console.WriteLine("After deletion: " + removedString);

        /**
        ** Now this is a very small example of data and operations on it.
        ** In real-world applications, data structures can be much more complex and optimized for various operations.
        ** Let's explore a more complex real-world application such as a book library system.
        ** We would want to store information about books, authors, and genres.
        ** We can use classes to represent these entities and collections to manage them.
        **/

        // Lets see the same operations but lets write them out
        Console.WriteLine("=== Arrays ===");
        int[] scores = new int[5];                // fixed size
        scores[0] = 10; scores[1] = 20; scores[2] = 30;

        // Traverse
        for (int i = 0; i < scores.Length; i++)
            Console.Write($"{scores[i]} ");
        Console.WriteLine();

        // Linear search
        int target = 20;
        bool found = false;
        for (int i = 0; i < scores.Length; i++)
            if (scores[i] == target) { found = true; break; }
        Console.WriteLine($"Found {target}? {found}");

        // Define a class to represent a book
        //     class Book
        // {
        //     public string Title { get; set; }
        //     public string Author { get; set; }
        //     public string Genre { get; set; }
        // }

        //     // Define a class to represent a library
        //     class Library
        //     {
        //         private List<Book> books = new List<Book>();

        //         // Add a book to the library
        //         public void AddBook(Book book)
        //         {
        //             books.Add(book);
        //         }

        //         // Find books by author
        //         public List<Book> FindBooksByAuthor(string author)
        //         {
        //             return books.FindAll(b => b.Author == author);
        //         }

        //         // Get all books
        //         public List<Book> GetAllBooks()
        //         {
        //             return books;
        //         }
        //     }
        // }
    }
}
