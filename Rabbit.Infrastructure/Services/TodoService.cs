using AutoMapper;
using MassTransit;
using Rabbit.Application.DTOs;
using Rabbit.Application.Interfaces;
using Rabbit.Domain.Entities;
using System.Text.Json;
using MediatR;
using Rabbit.Application.Events.TodoEvents;
using Rabbit.Application.Interfaces.Todos;
using Rabbit.Contracts.LogMessages;

namespace Rabbit.Infrastructure.Services;

public class TodoService(
    ITodoRepository todoRepository,
    IMapper mapper,
    IMediator mediator
) : ITodoService
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
        var result = mapper.Map<TodoDto>(todo);

        return result;
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