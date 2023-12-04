using System;

namespace lab4 
{
    class MagazinesChangedEventArgs<TKey> : EventArgs 
    {
        public string CollectionName { get; }
        public Update UpdateType { get; }
        public string ChangedProperty { get; }
        public TKey Key { get; }

        public MagazinesChangedEventArgs(string collectionName, Update updateType, string changedProperty, TKey key)
        {
            CollectionName = collectionName;
            UpdateType = updateType;
            ChangedProperty = changedProperty;
            Key = key;
        }

        public override string ToString()
        {
            return $"Collection: {CollectionName}, UpdateType: {UpdateType}, ChangedProperty: {ChangedProperty}, Key: {Key}";
        }
    }
}
