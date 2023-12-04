using System.Collections.Generic;
using System.Text;

namespace lab4 {
    class Listener 
    {
        private List<ListEntry> changed = new List<ListEntry>();

        public void OnMagazinesChanged(object sender, MagazinesChangedEventArgs<string> e)
        {
            ListEntry entry = new ListEntry(e.CollectionName, e.UpdateType, e.ChangedProperty, e.Key.ToString());
            changed.Add(entry);
        }

        public override string ToString()
        {
            string result = "List of Changes:\n";
            foreach (var entry in changed)
            {
                result += entry.ToString() + "\n";
            }
            return result;
        }
    }

    class ListEntry 
    {
        public string Name {get; set;}
        public Update Type {get; set;}
        public string ChangedProperty {get; set;}
        public string Key {get; set;}

        public ListEntry(string name, Update type, string changedProperty, string key) {
            Name = name;
            Type = type;
            ChangedProperty = changedProperty;
            Key = key;
        }

        public override string ToString()
    {
        return $"Collection: {Name}, UpdateType: {Type}, ChangedProperty: {ChangedProperty}, Key: {Key}";
    }
    }
}