using Coopers.BusinessLayer.Database.Domain.IRepositories;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.Repositories.Repository
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {

        #region PRIVATE MEMBERS

        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbset;

        #endregion


        #region CONSTRUCTOR

        public Repository(DbContext context)
        {
            _context = context;
            _dbset = context.Set<TEntity>();
        }

        #endregion


        #region CRUD METHODS

        #region SYNC METHODS

        public TEntity GetEntityById(TKey id, bool bIsAsTrackable = false)
        {
            if (bIsAsTrackable)
            {
                return _dbset.Find(id);
            }
            else
            {
                var entityExpression = Expression.Parameter(typeof(TEntity), "e");
                var memberExpression = Expression.Property(entityExpression, GetPrimaryKeyColumnName());
                var costExpression = Expression.Constant(id, typeof(TKey));

                Expression<Func<TEntity, bool>> predicate = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(memberExpression, costExpression), entityExpression);
                return GetFilteredEntities().Where(predicate).FirstOrDefault();
            }
        }

        public int Create(TEntity entity)
        {
            if (entity != null)
            {
                _dbset.Add(entity);
                _context.Entry(entity).State = EntityState.Added;
                return _context.SaveChanges();
            }
            return 0;
        }

        public int Update(TEntity entity)
        {
            if (entity != null)
            {
                _dbset.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                return _context.SaveChanges();
            }
            return 0;
        }

        public int DeleteEntityByID(TKey id)
        {
            TEntity entity = GetEntityById(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Deleted;
                return _context.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region ASYNC METHODS


        public async Task<int> CreateEntityAsync(TEntity entity)
        {
            if (entity != null && IsEntityValid(entity))
            {
                _dbset.Add(entity);
                _context.Entry(entity).State = EntityState.Added;
                return await _context.SaveChangesAsync();
            }
            return await Task.FromResult(0);
        }

        public async Task<TEntity> GetEntityByIdAsync(TKey id, bool bIsAsTrackable = false)
        {
            if (bIsAsTrackable)
            {
                return await ((DbSet<TEntity>)GetFilteredEntities(bIsAsTrackable)).FindAsync(id);
            }
            else
            {
                var entityExpression = Expression.Parameter(typeof(TEntity), "e");
                var memberExpression = Expression.Property(entityExpression, GetPrimaryKeyColumnName());
                var costExpression = Expression.Constant(id, typeof(TKey));
                var equalityExp = Expression.Equal(memberExpression, costExpression);

                Expression<Func<TEntity, bool>> predicate = Expression.Lambda<Func<TEntity, bool>>(equalityExp, entityExpression);
                Func<TEntity, bool> compiled = predicate.Compile();
                return await GetFilteredEntities(bIsAsTrackable).Where(predicate).FirstOrDefaultAsync();
            }
        }

        public async Task<int> DeleteEntityAsync(TEntity entity)
        {
            if (entity != null && IsEntityValid(entity))
            {
                _context.Entry(entity).State = EntityState.Deleted;
                return await _context.SaveChangesAsync();
            }
            else
            {
                return await Task.FromResult(0);
            }
        }

        public async Task<int> UpdateEntityAsync(TEntity entity)
        {
            if (entity != null && IsEntityValid(entity))
            {
                _dbset.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            return await Task.FromResult(0);
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await GetFilteredEntities().Where(filterExpression).AnyAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await GetFilteredEntities().AnyAsync(filterExpression);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await GetFilteredEntities().CountAsync(filterExpression);
        }

        #endregion

        #endregion


        #region VIRTUAL MEMBERS

        public virtual string GetPrimaryKeyColumnName()
        {
            return "id";
        }

        public virtual IQueryable<TEntity> GetFilteredEntities(bool bIsAsTrackable = false)
        {
            return _dbset;
        }

        public virtual bool IsEntityValid(TEntity entity)
        {
            return true;
        }

        #endregion


    }
}
