namespace Ssl.Certificate.Monitor.Interfaces
{
    internal interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? Get(int id);
        void Insert(T item);
        void Update(T item);
        void Delete(int d);
        void Save();
    }
}
