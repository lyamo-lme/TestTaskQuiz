using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TestTaskQuiz.Core.Data;

namespace TestTaskQuiz.Data;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<TEntity> _entity;

    public GenericRepository(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
        _entity = dbContext.Set<TEntity>();
    }


    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int? take = null, int? skip = null,
        string includeProperties = "")
    {
        try
        {
            IQueryable<TEntity> query = _entity;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                         (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip != null)
            {
                query = query.Skip((int)skip);
            }

            if (take != null)
            {
                query = query.Take((int)take);
            }

            return query;
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> func, string? relatedData = null)
    {
        try
        {
            IQueryable<TEntity> query = _entity;
            if (relatedData != null)
            {
                query = query.Include(relatedData);
            }

            return await query.FirstOrDefaultAsync(func);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public ValueTask<TEntity> CreateAsync(TEntity model)
    {
        try
        {
            var md = _entity.Add(model);
            return new ValueTask<TEntity>(md.Entity);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public Task<bool> DeleteAsync(TEntity entityToDelete)
    {
        try
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _entity.Attach(entityToDelete);
            }

            _entity.Remove(entityToDelete);
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            throw;
        }
    }


    public ValueTask<TEntity> UpdateAsync(TEntity model)
    {
        try
        {
            var entity = _entity.Update(model);
            _dbContext.Entry(model).State = EntityState.Modified;
            return new ValueTask<TEntity>(entity.Entity);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}