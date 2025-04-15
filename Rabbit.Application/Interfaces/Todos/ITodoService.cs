using Rabbit.Application.DTOs;

namespace Rabbit.Application.Interfaces.Todos;

public interface ITodoService
{
    Task<IEnumerable<TodoDto>> GetAllAsync();
    Task<TodoDto> GetByIdAsync(int todoId);
    Task<TodoDto> CreateAsync(TodoDto todoDto);
    Task<bool> UpdateAsync(int todoId, TodoDto todoDto);
    Task<bool> DeleteAsync(int todoId);
}