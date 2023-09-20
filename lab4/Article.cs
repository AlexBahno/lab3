using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Article : IRateAndCopy
    {
        public Person Author
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public double Rating
        {
            get;
            set;
        }
        public Article()
        {
            Author = new Person();
            Title = "title";
            Rating = 0;
        }

        public Article(Person author, string title, double rating)
        {
            Author = author;
            Title = title;
            Rating = rating;
        }

        public override string ToString()
        {
            return $"Author: {Author.ToShortString()}; Title: {Title}; Rating: {Rating}";
        }

        public object DeepCopy()
        {
            Article temp = (Article)MemberwiseClone();
            temp.Author = Author;
            temp.Title = Title;
            return temp;
        }
    }
}
