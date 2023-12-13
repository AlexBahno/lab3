using System;
using System.IO;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                Magazine magazine = new Magazine("New Magazine", Frequency.Monthly, new DateTime(2023, 1, 21), 300);
                magazine.AddArticles(new Article(new Person("John", "Fishman", new DateTime()), "New Article", 7.8));

                Magazine copy = magazine.DeepCopy();

                System.Console.WriteLine("Origin: " + magazine + "; Copy: " + copy);
                
                Console.WriteLine("\nEnter name of file:");
                string filename = Console.ReadLine();

                if (File.Exists(filename)) 
                {
                    magazine.Load(filename);
                    System.Console.WriteLine("Loaded magazine: " + magazine);
                } else {
                    Console.WriteLine($"File {filename} doesn`t exist. Creating file...");
                    File.Create(filename).Close();
                }

                magazine.AddFromConsole();
                magazine.Save(filename);
                System.Console.WriteLine("Magazine: " + magazine);

                Magazine.Load(filename, magazine);
                magazine.AddFromConsole();
                Magazine.Save(filename, magazine);

                System.Console.WriteLine("Magazine: " + magazine);
            } catch (Exception e) {
                System.Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
