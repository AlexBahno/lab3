using System;

namespace lab4 {
    class  MagazineListHandlerEventArgs : EventArgs {
        public string Name { get; set; }
        public string TypeOfAction { get; set; }
        public int Id { get; set; }

        public MagazineListHandlerEventArgs(string name, string type, int id) {
            Name = name;
            TypeOfAction = type;
            Id = id;
        }

        public override string ToString()
        {
            return $"Name: {Name}; Type: {TypeOfAction}; Id: {Id}";
        }
    }
}