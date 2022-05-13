using System.Collections.Generic;

namespace NwDataBase
{
    public interface IDataBaseReader<T>
    {
        IEnumerable<T> ItemsCollection { get; }
    }

    public interface IDataBaseWriter<T>
    {
        public bool AddItem(T item);
        public bool RemoveItem(T item);
        public void AddItems(IEnumerable<T> itemsCollection);
    }
}