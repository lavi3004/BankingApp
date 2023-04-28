using DataAccess.Abstactisations;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EF;

public class BaseRepository<T> : IBaseRepository<T> where T : ModelEntity
{
    protected readonly DbContext dbContext;
    public BaseRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public T Add(T entity)
    {
        var added = dbContext.Set<T>().Add(entity);
        dbContext.SaveChanges();
        return added.Entity;
    }

    public IEnumerable<T> GetAll()
    {
        return dbContext.Set<T>()
                        .ToList();
    }

    public void Remove(int entityId)
    {
        var element = dbContext.Set<T>()
                               .First(e => e.Id == entityId);
        dbContext.Remove(element);
        dbContext.SaveChanges();
    }
}
