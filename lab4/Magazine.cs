using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    [Serializable]
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
            set {
                OnPropertyChanged(nameof(FrequencyOfRelease)); 
                frequency = value;
            }
        }

        public List<Article> Articles
        {
            get => articles;
            set {
                OnPropertyChanged(nameof(Articles));
                articles = value; 
            } 
        }

        public List<Person> Authors
        {
            get => authorsList;
            set {
                OnPropertyChanged(nameof(Authors));
                authorsList = value;
            } 
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
                OnPropertyChanged(nameof(Edition));
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

        public new Magazine DeepCopy()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                SoapFormatter formatter = new SoapFormatter();
                formatter.Serialize(memoryStream, this);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return (Magazine)formatter.Deserialize(memoryStream);
            }
        }

        public bool Save(string filename)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filename, FileMode.Create))
                {
                    SoapFormatter formatter = new SoapFormatter();
                    formatter.Serialize(fileStream, this);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

         public bool Load(string filename)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filename, FileMode.Open))
                {
                    SoapFormatter formatter = new SoapFormatter();
                    var loadedObject = (Magazine)formatter.Deserialize(fileStream);
                    this.Name = loadedObject.Name;
                    this.ReleaseDate = loadedObject.ReleaseDate;
                    this.Amount = loadedObject.Amount;
                    this.FrequencyOfRelease = loadedObject.FrequencyOfRelease;
                    this.Articles = loadedObject.Articles;
                    this.Authors = loadedObject.Authors;
                }
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
        }

        public static bool Save(string filename, Magazine obj)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filename, FileMode.Create))
                {
                    SoapFormatter formatter = new SoapFormatter();
                    formatter.Serialize(fileStream, obj);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Load(string filename, Magazine obj)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filename, FileMode.Open))
                {
                    SoapFormatter formatter = new SoapFormatter();
                    var loadedObject = (Magazine)formatter.Deserialize(fileStream);
                    obj.Name = loadedObject.Name;
                    obj.ReleaseDate = loadedObject.ReleaseDate;
                    obj.Amount = loadedObject.Amount;
                    obj.FrequencyOfRelease = loadedObject.FrequencyOfRelease;
                    obj.Articles = loadedObject.Articles;
                    obj.Authors = loadedObject.Authors;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddFromConsole()
        {
            try
            {
                Console.WriteLine("Enter data in next way: Title, Name of Author, Date of Birthday(yyyy-mm-dd), Rating of Article");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Error: You enter empty data.");
                    return false;
                }

                string[] inputParts = input.Split(',');

                if (inputParts.Length != 4)
                {
                    Console.WriteLine("Error: Invalid format of data.");
                    return false;
                }

                string articleTitle = inputParts[0].Trim();
                string authorName = inputParts[1].Trim();
                string authorAge = inputParts[2].Trim();
                double articleRating = double.Parse(inputParts[3].Trim());

                int year = int.Parse(authorAge.Split('-')[0]);
                int month = int.Parse(authorAge.Split('-')[1]);
                int day = int.Parse(authorAge.Split('-')[2]);

                Person author = new Person(authorName.Split(' ')[0], authorName.Split(' ')[1], new DateTime(year, month, day));
                Article newArticle = new Article(author, articleTitle, articleRating);

                Articles.Add(newArticle);

                Console.WriteLine("Article was added.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
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
