using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class MagazineCollection
    {
        private List<Magazine> magazines;

        public string Name {
            get; set;
        }

        public MagazineCollection()
        {
            Magazines = new List<Magazine>();
        }

        public List<Magazine> Magazines {
            get => magazines;
            set => magazines = value;
        }

        public Magazine this[int index] {
            get => Magazines[index];
            set { 
                Magazines[index] = value;
                MagazineReplaced?.Invoke(
                    this,
                    new MagazineListHandlerEventArgs(Name, "Replaced", index)
                );
            }
        }

        public delegate void MagazineListHandler (object source, MagazineListHandlerEventArgs args);
        public event MagazineListHandler MagazineAdded;
        public event MagazineListHandler MagazineReplaced;

        public bool Replace(int j, Magazine mg) {
            if (0 <= j && j < Magazines.Count) {
                Magazines[j] = mg;
                MagazineReplaced?.Invoke(
                    this,
                    new MagazineListHandlerEventArgs(Name, "Replaced", j)
                );
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Magazines.ForEach(magazin => sb.Append(magazin.ToString()));
            return sb.ToString();
        }

        public string ToShortString()
        {
            StringBuilder sb = new StringBuilder();
            Magazines.ForEach(magazine => sb.Append(magazine.ToShortString()));
            return sb.ToString();
        }

        public void AddDefaults()
        {
            for (int i = 0; i < 4; i++) {
                Magazine mg = new Magazine(
                                    $"Magazine{i + 1}",
                                    Frequency.Monthly,
                                    new DateTime(2022, i + 1, 1),
                                    10000 * (i + 1)
                                    );
                Magazines.Add(mg);
                MagazineAdded?.Invoke(
                    this,
                    new MagazineListHandlerEventArgs(Name, "Added", Magazines.IndexOf(mg))
                );
            }
            // Magazines.AddRange(new Magazine[]
            // {
            //     new Magazine(),
            //     new Magazine("Forbes", Frequency.Weekly, new DateTime(2022, 1, 1), 1000000),
            //     new Magazine("Time", Frequency.Monthly, new DateTime(2022, 2, 1), 500000),
            //     new Magazine("National Geographic", Frequency.Yearly, new DateTime(2022, 3, 1), 750000),
            // });
        }

        public void AddMagazines(params Magazine[] newMagazines)
        {
            Magazines.AddRange(newMagazines);
            foreach (var magazine in newMagazines) {
                MagazineAdded?.Invoke(
                    this,
                    new MagazineListHandlerEventArgs(Name, "Added", Magazines.IndexOf(magazine))
                );
            }
        }

        public List<Magazine> SortByTitle()
        {
            Magazines.Sort();
            return Magazines;
        }

        public List<Magazine> SortByReleaseDate()
        {
            Magazines.Sort(new Edition().Compare);
            return Magazines;
        }

        public List<Magazine> SortByEditionAmount()
        {
            Magazines.Sort(new Edition.EditionComparer());
            return Magazines;
        }

    }
}
