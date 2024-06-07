using System.Linq.Expressions;

namespace ToDoListApp.Api.Interfaces;

public interface IRepository<T> where T: class
{
    public IEnumerable<T> GetAll();
    public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate);
    public T? Find(Expression<Func<T, bool>> predicate);
    public T? GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
}