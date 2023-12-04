using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    delegate TKey KeySelector<TKey>(Magazine mg);
    delegate void MagazinesChangedHandler<TKey>(object source, MagazinesChangedEventArgs<TKey> args);

    class MagazineCollection<TKey>
    {
        private Dictionary<TKey, Magazine> magazines;

        public string Name 
        {
            get; set;
        }

        public Dictionary<TKey, Magazine> Magazines 
        {
            get => magazines;
            set => magazines = value;
        }

        public MagazineCollection() 
        {
            Magazines = new Dictionary<TKey, Magazine>();
        }

        public MagazineCollection(KeySelector<TKey> keySelector) 
        {
            Magazines = new Dictionary<TKey, Magazine>();
            this.keySelector = keySelector;
        }

        private KeySelector<TKey> keySelector;
        public event MagazinesChangedHandler<TKey> MagazinesChanged;

        public void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e) {
            MagazinesChanged?.Invoke(
                sender,
                new MagazinesChangedEventArgs<TKey>(
                    Name, 
                    Update.Property, 
                    e.PropertyName, 
                    keySelector.Invoke(sender as Magazine)
                )
            );
        }

        public void ChangeMagazine(Magazine magazine) {
            Magazine m = Magazines.FirstOrDefault(m => m.Value == magazine).Value;
            if (m != null) {
            m.FrequencyOfRelease = Frequency.Monthly;
            m.ReleaseDate = new DateTime();
            m.Amount = 30;
            }
        }

        public void AddDefaults()
        {
            for (int i = 0; i < 4; i++) 
            {
                Magazine mg = new Magazine(
                                    $"Magazine{i + 1}",
                                    Frequency.Monthly,
                                    new DateTime(2022, i + 1, 1),
                                    10000 * (i + 1)
                                    );
                mg.PropertyChanged += PropertyChangedEventHandler;
                Magazines.Add(keySelector.Invoke(mg), mg);
                MagazinesChanged?.Invoke(this, new MagazinesChangedEventArgs<TKey>(Name, Update.Add, "-", keySelector.Invoke(mg)));
            }
            Magazine m = new Magazine();
            m.PropertyChanged += PropertyChangedEventHandler;
            Magazines.Add(keySelector.Invoke(m), m);
            MagazinesChanged?.Invoke(this, new MagazinesChangedEventArgs<TKey>(Name, Update.Add, "-", keySelector.Invoke(m)));

            m = new Magazine("Forbes", Frequency.Weekly, new DateTime(2022, 1, 1), 1000000);
            m.PropertyChanged += PropertyChangedEventHandler;
            Magazines.Add(
                keySelector.Invoke(m),
                m
            );
            MagazinesChanged?.Invoke(
                this, 
                new MagazinesChangedEventArgs<TKey>(
                    Name, 
                    Update.Add,
                    "-",
                    keySelector.Invoke(m)
                )
            );

            m = new Magazine("Time", Frequency.Monthly, new DateTime(2022, 2, 1), 500000);
            m.PropertyChanged += PropertyChangedEventHandler;
            Magazines.Add(
                keySelector.Invoke(m),
                m
            );
            MagazinesChanged?.Invoke(
                this, 
                new MagazinesChangedEventArgs<TKey>(
                    Name, 
                    Update.Add,
                    "-",
                    keySelector.Invoke(m)
                )
            );

            m = new Magazine("National Geographic", Frequency.Yearly, new DateTime(2022, 3, 1), 750000);
            m.PropertyChanged += PropertyChangedEventHandler;
            Magazines.Add(
                keySelector.Invoke(m),
                m
            );
            MagazinesChanged?.Invoke(
                this, 
                new MagazinesChangedEventArgs<TKey>(
                    Name, 
                    Update.Add,
                    "-",
                    keySelector.Invoke(m)
                )
            );
        }

        public void AddMagazines(params Magazine[] newMagazines)
        {
            foreach (var magazine in newMagazines) 
            {
                magazine.PropertyChanged += PropertyChangedEventHandler;
                Magazines.Add(keySelector.Invoke(magazine), magazine);
                MagazinesChanged?.Invoke(
                this, 
                new MagazinesChangedEventArgs<TKey>(
                    Name,
                    Update.Add,
                    "-",
                    keySelector.Invoke(magazine)
                )
            );
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var magazine in Magazines) 
            {
                sb.Append("Key: " + magazine.Key.ToString() + "; Value: " + magazine.Value.ToString() + "; ");
            }
            return sb.ToString();
        }

        public string ToShortString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var magazine in Magazines) 
            {
                sb.Append("Key: " + magazine.Key.ToString() + "; Value: " + magazine.Value.ToShortString() + "; ");
            }
            return sb.ToString();
        }

        public double MaxRating {
            get 
            {
                if (Magazines.Count == 0) 
                {
                    return 0;
                }
                double max = Magazines.Values.Max(m => m.AverageRating);
                return max;
            }
        }

        public IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency value)
        {
            return Magazines.Where(m => m.Value.FrequencyOfRelease == value);
        }

        public IEnumerable<IGrouping<Frequency,KeyValuePair<TKey,Magazine>>> GroupByFrequency
        {
            get 
            {
                return Magazines.GroupBy(m => m.Value.FrequencyOfRelease);
            }
        }

        public bool Replace(Magazine mOld, Magazine mNew)
        {
            var key = Magazines.FirstOrDefault(x => x.Value == mOld).Key;
            if (key != null)
            {
                Magazines[key].PropertyChanged -= PropertyChangedEventHandler;
                Magazines[key] = mNew;
                MagazinesChanged?.Invoke(
                    this,
                    new MagazinesChangedEventArgs<TKey>(
                    Name, 
                    Update.Replace,
                    "-",
                    keySelector.Invoke(mNew)
                    )
                );
                return true;
            }
            return false;
        }
    }
}