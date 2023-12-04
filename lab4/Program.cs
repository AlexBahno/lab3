using System;
using System.Collections.Generic;
using System.Linq;

namespace lab4
{
    class Program
    {
        static String keyMaker(Magazine mg) {
            return mg.Name;
        }

        static void Main(string[] args)
        {
            MagazineCollection<String> localeCollection = new MagazineCollection<string>(keyMaker);
            MagazineCollection<String> foreignCollection = new MagazineCollection<string>(keyMaker);
            localeCollection.Name = "Locale Collection";
            foreignCollection.Name = "Foreign Collection";

            Listener localeListener = new Listener();
            Listener foreignListener = new Listener();

            localeCollection.MagazinesChanged += localeListener.OnMagazinesChanged;
            foreignCollection.MagazinesChanged += foreignListener.OnMagazinesChanged;

            localeCollection.AddDefaults();
            foreignCollection.AddDefaults();

            Magazine localeMagazine = new Magazine("Locale magazine", Frequency.Weekly, new DateTime(), 1000);
            Magazine foreignMagazine = new Magazine("Foreign magazine", Frequency.Weekly, new DateTime(), 1000);

            localeCollection.AddMagazines(localeMagazine);
            foreignCollection.AddMagazines(foreignMagazine);

            localeCollection.ChangeMagazine(localeMagazine);
            foreignCollection.ChangeMagazine(foreignMagazine);

            Magazine anotherMagazine = new Magazine("Replace Magazine", Frequency.Yearly, new DateTime(), 30);
            anotherMagazine.AddArticles(new Article(new Person(), "Title", 10));

            localeCollection.Replace(localeMagazine, anotherMagazine);
            foreignCollection.Replace(foreignMagazine, anotherMagazine);

            localeCollection.ChangeMagazine(localeMagazine);
            foreignCollection.ChangeMagazine(foreignMagazine);

            System.Console.WriteLine(localeListener);
            System.Console.WriteLine(foreignListener);


            System.Console.WriteLine(localeCollection.MaxRating + "\n");
            
            foreach (KeyValuePair<string, Magazine> pair in localeCollection.FrequencyGroup(Frequency.Monthly)) {
                System.Console.WriteLine("--------------------------------------------------");
                System.Console.WriteLine(pair.Value);
                System.Console.WriteLine("--------------------------------------------------");
            }

            foreach (IGrouping<Frequency, KeyValuePair<string, Magazine>> group in localeCollection.GroupByFrequency) {
                System.Console.WriteLine("-----------------------------------------------------");
                System.Console.WriteLine(group.Key);
                foreach (KeyValuePair<string, Magazine> pair in group) {
                    System.Console.WriteLine(pair.Value);
                }
                System.Console.WriteLine("-----------------------------------------------------");
            }
        }
    }
}
