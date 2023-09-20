using System.Collections.Generic;
using System.Text;

namespace lab4 {
    class Listener {
        public List<ListEnrty> changed = new List<ListEnrty>();

        public void MagazineAdded(object sender, MagazineListHandlerEventArgs e) {
            ListEnrty listEnrty = new ListEnrty(e.Name, e.TypeOfAction, e.Id);
            changed.Add(listEnrty);
        }

        public void MagazineReplaced(object sender, MagazineListHandlerEventArgs e) {
            ListEnrty listEnrty = new ListEnrty(e.Name, e.TypeOfAction, e.Id);
            changed.Add(listEnrty);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            changed.ForEach(le => sb.Append(le.ToString()));
            return sb.ToString();
        }
    }

    class ListEnrty {
        public string Name { get; }
        public string TypeOfAction { get; }
        public int Id { get; }

        public ListEnrty(string name, string typeOfAction, int id) {
            Name = name;
            TypeOfAction = typeOfAction;
            Id = id;
        }

        override public string ToString() {
            return $"Name: {Name}; Type of Action: {TypeOfAction}; Id: {Id}\n";
        }
    }
}