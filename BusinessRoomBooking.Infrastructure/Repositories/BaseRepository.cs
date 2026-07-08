using BusinessRoomBooking.Core.Interfaces.Repositories;
using BusinessRoomBooking.Infrastructure.DataBase.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessRoomBooking.Infrastructure.Repositories;

public class BaseRepository<T>(BusinessRoomBookingContext context) : IBaseRepository<T> where T : class
{
  protected readonly BusinessRoomBookingContext Context = context;
  protected readonly DbSet<T> DbSet = context.Set<T>();

  public async Task<T?> GetByIdAsync(Guid id)
  {
    return await DbSet.FindAsync(id);
  }

  public async Task<IEnumerable<T>> GetAllAsync()
  {
    return await DbSet.ToListAsync();
  }

  public async Task AddAsync(T entity)
  {
    await DbSet.AddAsync(entity);
  }

  public Task UpdateAsync(T entity)
  {
    DbSet.Update(entity);
    return Task.CompletedTask;
  }

  public async Task DeleteAsync(Guid id)
  {
    T? entity = await GetByIdAsync(id);
    if (entity is not null)
    {
      DbSet.Remove(entity);
    }
  }

  public async Task SaveChangesAsync()
  {
    await Context.SaveChangesAsync();
  }
}