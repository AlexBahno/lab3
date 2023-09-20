using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class TestCollections
    {
        private List<Edition> editions;
        private List<string> stringList;
        private Dictionary<Edition, Magazine> editionDict;
        private Dictionary<string, Magazine> stringDict;

        public TestCollections(int i)
        {
            editions = new List<Edition>();
            stringList = new List<string>();
            editionDict = new Dictionary<Edition, Magazine>();
            stringDict = new Dictionary<string, Magazine>();

            for (int j = 0; j < i; j++)
            {
                editions.Add(defaultElement(j));
                stringList.Add(defaultElement(j).ToString());
                editionDict.Add(defaultElement(j).Edition, defaultElement(j));
                stringDict.Add(defaultElement(j).ToString(), defaultElement(j));
            }
        }

        public static Magazine defaultElement(int i)
        {
            Magazine m = new Magazine("T", Frequency.Weekly, new DateTime(2000,1,1), 1000);
            m.Name = $"{i}";
            return m;
        }

        public void SeachElem()
        {
            var sw = new Stopwatch();

            sw.Start();
            bool b1 = editions.Contains(defaultElement(0));
            sw.Stop();
            Console.WriteLine(b1);
            Console.WriteLine("Search first element in Edition list: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b2 = stringList.Contains(defaultElement(0).ToString());
            sw.Stop();
            Console.WriteLine(b2);
            Console.WriteLine("Search first element in string list: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b3 = editionDict.ContainsValue(defaultElement(0));
            sw.Stop();
            Console.WriteLine(b3);
            Console.WriteLine("Search first value in Edition dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b4 = stringDict.ContainsValue(defaultElement(0));
            sw.Stop();
            Console.WriteLine(b4);
            Console.WriteLine("Search first value in string dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b5 = editionDict.ContainsKey(defaultElement(0).Edition);
            sw.Stop();
            Console.WriteLine(b5);
            Console.WriteLine("Search first element by key in Edition dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b6 = stringDict.ContainsKey(defaultElement(0).ToString());
            sw.Stop();
            Console.WriteLine(b6);
            Console.WriteLine("Search first element by key in string dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            Console.WriteLine("----------------------------------------------------------------------------------------------");

            sw.Start();
            bool b7 = editions.Contains(defaultElement(editions.Count / 2));
            sw.Stop();
            Console.WriteLine(b7);
            Console.WriteLine("Search middle element in Edition list: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b8 = stringList.Contains(defaultElement(editions.Count / 2).ToString());
            sw.Stop();
            Console.WriteLine(b8);
            Console.WriteLine("Search middle element in string list: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b9 = editionDict.ContainsValue(defaultElement(editions.Count / 2));
            sw.Stop();
            Console.WriteLine(b9);
            Console.WriteLine("Search middle value in Edition dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b10 = stringDict.ContainsValue(defaultElement(editions.Count / 2));
            sw.Stop();
            Console.WriteLine(b10);
            Console.WriteLine("Search middle value in string dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b11 = editionDict.ContainsKey(defaultElement(editions.Count / 2).Edition);
            sw.Stop();
            Console.WriteLine(b11);
            Console.WriteLine("Search middle element by key in Edition dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b12 = stringDict.ContainsKey(defaultElement(editions.Count / 2).ToString());
            sw.Stop();
            Console.WriteLine(b12);
            Console.WriteLine("Search middle element by key in string dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            Console.WriteLine("----------------------------------------------------------------------------------------------");

            sw.Start();
            bool b13 = editions.Contains(defaultElement(editions.Count - 1));
            sw.Stop();
            Console.WriteLine(b13);
            Console.WriteLine("Search last element in Edition list: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b14 = stringList.Contains(defaultElement(editions.Count - 1).ToString());
            sw.Stop();
            Console.WriteLine(b14);
            Console.WriteLine("Search last element in string list: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b15 = editionDict.ContainsValue(defaultElement(editions.Count - 1));
            sw.Stop();
            Console.WriteLine(b15);
            Console.WriteLine("Search last value in Edition dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b16 = stringDict.ContainsValue(defaultElement(editions.Count - 1));
            sw.Stop();
            Console.WriteLine(b16);
            Console.WriteLine("Search last value in string dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b17 = editionDict.ContainsKey(defaultElement(editions.Count - 1).Edition);
            sw.Stop();
            Console.WriteLine(b17);
            Console.WriteLine("Search last element by key in Edition dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b18 = stringDict.ContainsKey(defaultElement(editions.Count - 1).ToString());
            sw.Stop();
            Console.WriteLine(b18);
            Console.WriteLine("Search last element by key in string dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            Console.WriteLine("----------------------------------------------------------------------------------------------");

            sw.Start();
            bool b19 = editions.Contains(defaultElement(editions.Count + 1));
            sw.Stop();
            Console.WriteLine(b19);
            Console.WriteLine("Search non-existent element in Edition list: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b20 = stringList.Contains(defaultElement(editions.Count + 1).ToString());
            sw.Stop();
            Console.WriteLine(b20);
            Console.WriteLine("Search non-existent element in string list: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b21 = editionDict.ContainsValue(defaultElement(editions.Count + 1));
            sw.Stop();
            Console.WriteLine(b21);
            Console.WriteLine("Search non-existent value in Edition dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b22 = stringDict.ContainsValue(defaultElement(editions.Count + 1));
            sw.Stop();
            Console.WriteLine(b22);
            Console.WriteLine("Search non-existent value in string dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b23 = editionDict.ContainsKey(defaultElement(editions.Count + 1).Edition);
            sw.Stop();
            Console.WriteLine(b23);
            Console.WriteLine("Search non-existent element by key in Edition dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            bool b24 = stringDict.ContainsKey(defaultElement(editions.Count + 1).ToString());
            sw.Stop();
            Console.WriteLine(b24);
            Console.WriteLine("Search non-existent element by key in string dictionary: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();

        }
    }
}
