using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Drill.Server.Database;

public abstract class AbstractRepository<T>(PostgreSqlContext context, ILoggerFactory loggerFactory) where T : AbstractModel
{
    protected readonly DbSet<T> DbModel = context.Set<T>();

    protected readonly ILogger<T> Logger = loggerFactory.CreateLogger<T>();

    protected readonly PostgreSqlContext Context = context;

    
    async protected Task<T> FindOne(int id)
    {
        var model = await DbModel.FindAsync(id);
        return model;
    }

    async protected Task<T> CreateModelAsync(T model)
    {
        await Context.AddAsync(model);
        var result = await Context.SaveChangesAsync();
        if (result == 0)
        {
            throw new Exception("Db error. Not Create any model");
        }

        return model;
    }
    

    async protected Task<ImmutableArray<T>> CreateBulkModelsAsync(ImmutableArray<T> collection)
    {
        await Context.AddRangeAsync(collection);
        var result = await Context.SaveChangesAsync();
        if (result == 0)
        {
            throw new Exception("Db error. Not Created Bulk models");
        }

        return collection;
    }
    
    
    async protected Task<ImmutableArray<T>> UpdateBulkModelsAsync(ImmutableArray<T> collection)
    {
        Context.UpdateRange(collection);
        var result = await Context.SaveChangesAsync();
        if (result == 0)
        {
            throw new Exception("Db error. Not Updated Bulk models");
        }

        return collection;
    }


    protected Task<int> UpdateModelAsync(T model)
    {
        DbModel.Update(model);
        return Context.SaveChangesAsync();
    }
    
    
    private void Delete(T entity)
    {
        DbModel.Remove(entity);
    }
    
    
    async protected Task DeleteModel(T model)
    {
        Delete(model);
        var result = await Context.SaveChangesAsync();
        if (result == 0)
        {
            throw new Exception("Db error. Not deleted");
        }
    }
    
    
    async protected Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        DbModel.RemoveRange(entities);
        var result = await Context.SaveChangesAsync();
        if (result == 0)
        {
            throw new Exception("Db error. Not deleted any entities in bulk");
        }
    }
}