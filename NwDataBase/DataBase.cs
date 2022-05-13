using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace NwDataBase
{
    public abstract class AbstactDataBase
    {
        public abstract Type ItemType { get; }
    }

    public class DataBase<T> : AbstactDataBase, IDataBaseReader<T>, IDataBaseWriter<T>
    {
        private BlockingCollection<T> _itemsCollection = new BlockingCollection<T>();
        public IEnumerable<T> ItemsCollection => _itemsCollection.ToArray();

        public DataBase()
        { }

        public override Type ItemType
        {
            get => typeof(T);
        }

        public bool AddItem(T item)
            => _itemsCollection.TryAdd(item);

        public bool RemoveItem(T item)
            => _itemsCollection.TryTake(out _);

        public void AddItems(IEnumerable<T> itemsCollection)
        {
            foreach (var item in itemsCollection)
            {
                AddItem(item);
            }
        }
    }
}