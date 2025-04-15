using Microsoft.EntityFrameworkCore;
using Rabbit.Application.Interfaces.Todos;
using Rabbit.Domain.Entities;
using Rabbit.Infrastructure.Data;
using Rabbit.Infrastructure.Repositories.Generic;

namespace Rabbit.Infrastructure.Repositories;

public class TodoRepository(AppDbContext context) : GenericRepository<Todo>(context), ITodoRepository
{
    public async Task<IEnumerable<Todo>> GetCompletedTodosAsync()
    {
        return await _dbSet.Where(t => t.IsCompleted).ToListAsync();
    }
}