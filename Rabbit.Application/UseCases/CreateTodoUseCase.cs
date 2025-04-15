using Rabbit.Application.DTOs;
using Rabbit.Application.Interfaces;
using Rabbit.Application.Interfaces.Todos;

namespace Rabbit.Application.UseCases;

public interface ICreateTodoUseCase
{
    Task<TodoDto> ExecuteAsync(TodoDto dto);
}

public class CreateTodoUseCase(ITodoService todoService) : ICreateTodoUseCase
{
    //NOTE: Can create CreateTodoDto if u want
    public async Task<TodoDto> ExecuteAsync(TodoDto dto)
    {
        var result = await todoService.CreateAsync(dto);

        return result;
    }
}