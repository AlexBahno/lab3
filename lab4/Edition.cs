using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Edition : IComparable<Edition>, IComparer<Edition>, INotifyPropertyChanged
    {
        protected string name;
        protected DateTime releaseDate;
        protected int amount;

        public event PropertyChangedEventHandler PropertyChanged;

        public Edition()
        {
            Name = "Magazine`s title";
            ReleaseDate = DateTime.Now;
            Amount = 1000;
        }

        public Edition(string name, DateTime releaseDate, int amount)
        {
            Name = name;
            ReleaseDate = releaseDate;
            Amount = amount;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public DateTime ReleaseDate
        {
            get => releaseDate;
            set 
            {
                releaseDate = value;
                OnPropertyChanged(nameof(ReleaseDate));
            } 
        }

        public int Amount
        {
            get => amount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Amount of edition cannot be less then 0");
                }

                amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            if (ReferenceEquals(this, obj))
                return true;

            Edition edition = (Edition)obj;
            return edition.Name.Equals(Name) && edition.ReleaseDate.Equals(ReleaseDate) && edition.Amount == Amount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, ReleaseDate, Amount);
        }

        public override string ToString()
        {
            return $"Title: {Name}; Release Date: {ReleaseDate}; Amount: {Amount}";
        }

        public virtual object DeepCopy()
        {
            Edition temp = (Edition)MemberwiseClone();
            temp.Name = Name;
            temp.ReleaseDate = ReleaseDate;
            return temp;
        }

        public int CompareTo(Edition other)
        {
            return String.Compare(Name, other.Name);
        }

        public int Compare(Edition x, Edition y)
        {
            if(x == null || y == null)
            {
                throw new ArgumentException("There is null object");
            }
            return DateTime.Compare(x.ReleaseDate, y.ReleaseDate);
        }

        public class EditionComparer : IComparer<Edition>
        {
            public int Compare(Edition x, Edition y)
            {
                if (x == null || y == null)
                {
                    throw new ArgumentException("There is null object");
                }
                return y.Amount.CompareTo(x.Amount);
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
