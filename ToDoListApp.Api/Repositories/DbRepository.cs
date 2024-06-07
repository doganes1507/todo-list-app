using System.Linq.Expressions;
using ToDoListApp.Api.Data;
using ToDoListApp.Api.Interfaces;

namespace ToDoListApp.Api.Repositories;

public class DbRepository<T> (DataContext context) : IRepository<T> where T: class
{
    public IEnumerable<T> GetAll()
    {
        return context.Set<T>().ToList();
    }

    public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate)
    {
        return context.Set<T>().Where(predicate).ToList();
    }
    
    public T? Find(Expression<Func<T, bool>> predicate)
    {
        return context.Set<T>().FirstOrDefault(predicate);
    }

    public T? GetById(int id)
    {
        return context.Set<T>().Find(id);
    }

    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
        context.SaveChanges();
    }

    public void Update(T entity)
    {
        context.Set<T>().Update(entity);
        context.SaveChanges();
    }

    public void Remove(T entity)
    {
        context.Set<T>().Remove(entity);
        context.SaveChanges();
    }
}