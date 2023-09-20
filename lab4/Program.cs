using System;
using System.Collections.Generic;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            MagazineCollection localMagazines = new MagazineCollection();
            localMagazines.Name = "Local Collection";
            MagazineCollection internationalMagazines = new MagazineCollection();
            internationalMagazines.Name = "International Collection";

            Listener localColleListener = new Listener();
            Listener interColleListener = new Listener();

            localMagazines.MagazineAdded += localColleListener.MagazineAdded;
            localMagazines.MagazineReplaced += localColleListener.MagazineReplaced;

            internationalMagazines.MagazineAdded += interColleListener.MagazineAdded;
            internationalMagazines.MagazineReplaced += interColleListener.MagazineReplaced;

            localMagazines.AddDefaults();
            internationalMagazines.AddDefaults();

            localMagazines.Replace(2, new Magazine());
            internationalMagazines.Replace(3, new Magazine());

            localMagazines[0] = new Magazine();
            internationalMagazines[0] = new Magazine();

            System.Console.WriteLine(localColleListener.ToString());
            System.Console.WriteLine(interColleListener.ToString());
        }
    }
}
