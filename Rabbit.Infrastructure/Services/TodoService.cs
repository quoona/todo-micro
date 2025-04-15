using AutoMapper;
using MassTransit;
using Rabbit.Application.DTOs;
using Rabbit.Application.Interfaces;
using Rabbit.Contracts.Contracts;
using Rabbit.Domain.Entities;
using System.Text.Json;
using Rabbit.Application.Interfaces.Todos;

namespace Rabbit.Infrastructure.Services;

public class TodoService(
    ITodoRepository todoRepository,
    IMapper mapper,
    IPublishEndpoint publishEndpoint) : ITodoService
{
    public async Task<IEnumerable<TodoDto>> GetAllAsync()
    {
        var todos = await todoRepository.GetAllAsync();
        return mapper.Map<List<TodoDto>>(todos);
    }

    public async Task<TodoDto> GetByIdAsync(int todoId)
    {
        var todo = await todoRepository.GetByIdAsync(todoId);
        return mapper.Map<TodoDto>(todo);
    }

    public async Task<TodoDto> CreateAsync(TodoDto todoDto)
    {
        var todo = mapper.Map<Todo>(todoDto);
        await todoRepository.AddAsync(todo);

        await publishEndpoint.Publish(new AuditLogMessage
        {
            Action = "CreateAsync",
            UserId = todoDto.GuidId.ToString(),
            Data = JsonSerializer.Serialize(todoDto),
            Timestamp = DateTime.Now
        });

        return mapper.Map<TodoDto>(todo);
    }

    public async Task<bool> UpdateAsync(int todoId, TodoDto todoDto)
    {
        var todo = await todoRepository.GetByIdAsync(todoId);
        if (todo == null) return false;

        todo.Title = todoDto.Title;
        todo.IsCompleted = todoDto.IsComplete;

        await todoRepository.UpdateAsync(todo);
        return true;
    }

    public async Task<bool> DeleteAsync(int todoId)
    {
        var todo = await todoRepository.GetByIdAsync(todoId);
        if (todo == null) return false;

        await todoRepository.DeleteAsync(todo);
        return true;
    }
}