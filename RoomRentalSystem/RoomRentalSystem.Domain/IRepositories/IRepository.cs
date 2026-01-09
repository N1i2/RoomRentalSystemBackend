using RoomRentalSystem.Domain.Entities;
using System.Linq.Expressions;

namespace RoomRentalSystem.Domain.IRepositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null); 
    Task AddAsync(T? entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Expression<Func<T?, bool>> predicate);
}