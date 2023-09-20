using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class Person
    {
        private string firstName;
        private string secondName;
        private DateTime dateOfBirthday;

        public Person() { }

        public Person(string firstName, string secondName, DateTime dateOfBirthday)
        {
            this.firstName = firstName;
            this.secondName = secondName;
            this.dateOfBirthday = dateOfBirthday;
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string SecondName
        {
            get { return secondName; }
            set { secondName = value; }
        }

        public DateTime DateOfBirthday
        {
            get { return dateOfBirthday; }
            set { dateOfBirthday = value; }
        }

        public int YearOfBirthday
        {
            get { return dateOfBirthday.Year; }
            set
            {
                dateOfBirthday = new DateTime(value, dateOfBirthday.Month, dateOfBirthday.Day);
            }
        }       

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            
            Person person = (Person)obj;
            return firstName.Equals(person.FirstName) && 
                   secondName.Equals(person.SecondName) &&
                   dateOfBirthday.Equals(person.DateOfBirthday);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(firstName, secondName, dateOfBirthday);
        }

        public override string ToString()
        {
            return $"First name: {firstName}; Second name: {secondName}; Date of birthday: {dateOfBirthday}";
        }

        public static bool operator ==(Person person1, Person person2)
        {
            if (person1 == null)
            {
                if (person2 == null)
                {
                    return true;
                }
                return false;
            }
            return person1.Equals(person2);
        }

        public static bool operator !=(Person person1, Person person2)
        {
            return !(person1 == person2);
        }
        
        public virtual Person DeepCopy()
        {
            Person temp = (Person)MemberwiseClone();
            temp.FirstName = firstName;
            temp.SecondName = secondName;
            temp.DateOfBirthday = dateOfBirthday;
            return temp;
        }

        public string ToShortString()
        {
            return $"{firstName} {secondName}";
        }
    }
}
