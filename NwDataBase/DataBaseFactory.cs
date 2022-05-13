using System.Collections.Generic;
using System.Linq;

namespace NwDataBase
{
    public class DataBaseFactory
    {
        private static object _sync = new object();
        private static DataBaseFactory? _dataBaseFactory;
        private List<AbstactDataBase> _dataBasesCollection = new List<AbstactDataBase>();

        public static DataBaseFactory Instance
        {
            get
            {
                lock (_sync)
                {
                    if (_dataBaseFactory == null)
                    {
                        _dataBaseFactory = new DataBaseFactory();
                    }
                    return _dataBaseFactory;
                }
            }
        }

        private DataBaseFactory()
        { }

        public DataBase<T> GetDataBase<T>()
        {
            lock (_sync)
            {
                var dataBase = _dataBasesCollection.FirstOrDefault(db => db?.ItemType == typeof(T));
                if (dataBase == null)
                {
                    dataBase = new DataBase<T>();
                    _dataBasesCollection.Add(dataBase);
                }
                return (DataBase<T>)dataBase;
            }
        }

        public IDataBaseReader<T> GetDataBaseReader<T>()
        {
            return GetDataBase<T>();
        }

        public IDataBaseWriter<T> GetDataBaseWriter<T>()
        {
            return GetDataBase<T>();
        }
    }
}