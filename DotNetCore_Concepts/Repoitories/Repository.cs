using DotNetCore_Concepts.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore_Concepts.Repoitories
{
    public interface IRepository<T>
    {
        T Get(int id);
        Task<T> GetAsync(int id);
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
        void Add(T entity);
        Task AddAsync(T entity);
        void SaveChanges();
        Task SaveChangesAsync();
    }
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private DbSet<T> _dbSets;
        protected DbSet<T> DbSets => _dbSets ?? (_dbSets = _dbContext.Set<T>());
        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T Get(int id)
        {
            return DbSets.Find(id);
        }
        public virtual async Task<T> GetAsync(int id)
        {
            return await DbSets.FindAsync(id);
        }
        public List<T> GetAll()
        {
            return DbSets.ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await  DbSets.ToListAsync();
        }

        public void Add(T entity)
        {
            DbSets.Add(entity);
        }
        public async Task AddAsync(T entity)
        {
            await DbSets.AddAsync(entity);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
