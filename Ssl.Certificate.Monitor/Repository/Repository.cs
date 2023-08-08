using Microsoft.EntityFrameworkCore;
using Ssl.Certificate.Data;
using Ssl.Certificate.Monitor.Interfaces;

namespace Ssl.Certificate.Monitor.Repository
{
    internal class Repository<T> : IRepository<T> where T: class
    {
        private readonly MonitorDbContext _dbContext;
        private DbSet<T> _dbSet;
        public Repository(MonitorDbContext dbContext) {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T? Get(int id) => _dbSet.Find(id);

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T? entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
