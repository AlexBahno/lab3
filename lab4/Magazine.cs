using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Magazine : Edition, IRateAndCopy
    {
        private Frequency frequency;
        private List<Article> articles;
        private List<Person> authorsList;

        public Magazine()
        {
            FrequencyOfRelease = Frequency.Monthly;
            Articles = new List<Article>();
            Authors = new List<Person>();
        }

        public Magazine(string title, Frequency frequency, DateTime releaseDate, int edition) : base(title, releaseDate, edition)
        {
            FrequencyOfRelease = frequency;
            Articles = new List<Article>();
            Authors = new List<Person>();
        }

        public Frequency FrequencyOfRelease
        {
            get { return frequency; }
            set { frequency = value; }
        }

        public List<Article> Articles
        {
            get => articles;
            set => articles = value;
        }

        public List<Person> Authors
        {
            get => authorsList;
            set => authorsList = value;
        }

        public double AverageRating
        {
            get
            {
                if (articles.Count == 0)
                    return 0;
                return Articles.Average(article => article.Rating);
            }
        }

        public double Rating => AverageRating;

        public Edition Edition
        {
            get => new(name, releaseDate, amount);
            set
            {
                Name = value.Name;
                ReleaseDate = value.ReleaseDate;
                Amount = value.Amount;
            }
        }

        public override string ToString()
        {
            string allArticles = "";
            Articles.ForEach(article => allArticles += "{" + article + "}");
            string allAuthors = "";
            Authors.ForEach(author => allAuthors += "{" + author + "}");
            return $"M`s Title: {Name}; Frequency: {FrequencyOfRelease}; Release Date: {ReleaseDate}; Edition: {Amount}; List of Articles:\n{allArticles}; List of Authors: \n{allAuthors}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            if (ReferenceEquals(this, obj))
                return true;

            Magazine magazine = (Magazine)obj;
            return magazine.Name == Name && magazine.ReleaseDate.Equals(ReleaseDate)
                                         && magazine.Amount == Amount
                                         && magazine.FrequencyOfRelease.Equals(FrequencyOfRelease)
                                         && magazine.Articles.SequenceEqual(Articles)
                                         && magazine.Authors.SequenceEqual(Authors);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FrequencyOfRelease, Articles, Authors);
        }

        public string ToShortString()
        {
            return $"M`s Title: {Name}; Frequency: {FrequencyOfRelease}; Release Date: {ReleaseDate}; Edition: {Amount}; Average Rating of Articles: {AverageRating};" +
                $" Amount of authours: {authorsList.Count}; Amount of Articles: {articles.Count}; \n";
        }


        public IEnumerable<Article> getArticlesWithMoreRating(double rating)
        {
            for (int i = 0; i < articles.Count; i++)
            {
                if (articles[i].Rating > rating)
                {
                    yield return articles[i];
                }
            }
        }

        public IEnumerable<Article> getTitleOfArticlesWithContent(String title)
        {
            for (int i = 0; i < articles.Count; i++)
            {
                if (articles[i].Title.Contains(title))
                {
                    yield return articles[i];
                }
            }
        }

        public override object DeepCopy()
        {
            Magazine temp = (Magazine)MemberwiseClone();
            temp.Name = Name;
            temp.ReleaseDate = ReleaseDate;
            temp.FrequencyOfRelease = FrequencyOfRelease;
            temp.Articles = Articles.Select(article => new Article(article.Author, article.Title, article.Rating)).ToList();
            temp.Authors = Authors.Select(author => new Person(author.FirstName, author.SecondName, author.DateOfBirthday)).ToList();
            return temp;
        }

        public void AddArticles(params Article[] listOfArticles)
        {
            articles.AddRange(listOfArticles);
        }

        public void AddEditors(params Person[] listOfAuthors)
        {
            authorsList.AddRange(listOfAuthors);
        }
    }
}
