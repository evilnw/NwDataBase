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
        private BlockingCollection<T> _items = new BlockingCollection<T>();
        public IEnumerable<T> Items => _items.ToArray();

        public DataBase()
        { }

        public override Type ItemType
        {
            get => typeof(T);
        }

        public bool AddItem(T item)
            => _items.TryAdd(item);

        public bool RemoveItem(T item)
            => _items.TryTake(out item);

        public void AddItems(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                AddItem(item);
            }
        }
    }
}