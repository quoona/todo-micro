using Rabbit.Application.Interfaces.Generic;
using Rabbit.Domain.Entities;

namespace Rabbit.Application.Interfaces.Todos;

public interface ITodoRepository : IGenericRepository<Todo>
{
    // Custom thêm
    Task<IEnumerable<Todo>> GetCompletedTodosAsync();
}