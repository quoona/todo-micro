﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rabbit.Application.DTOs;
using Rabbit.Application.Interfaces;
using Rabbit.Domain.Entities;
using Rabbit.Infrastructure.Data;

namespace Rabbit.Infrastructure.Services;

public class TodoService(AppDbContext context, IMapper mapper) : ITodoService
{
    public async Task<IEnumerable<TodoDto>> GetAllAsync()
    {
        var todos = await context.Todos.ToListAsync();
        return mapper.Map<List<TodoDto>>(todos);
    }

    public async Task<TodoDto> GetByIdAsync(int todoId)
    {
        var todo = await context.Todos.FindAsync(todoId);
        return mapper.Map<TodoDto>(todo);
    }

    public async Task<TodoDto> CreateAsync(TodoDto todoDto)
    {
        var todo = mapper.Map<Todo>(todoDto);
        await context.Todos.AddAsync(todo);
        await context.SaveChangesAsync();

        return mapper.Map<TodoDto>(todo);
    }

    public async Task<bool> UpdateAsync(int todoId, TodoDto todoDto)
    {
        var todo = await context.Todos.FindAsync(todoId);
        if (todo == null) return false;

        todo.Title = todoDto.Title;
        todo.IsCompleted = todoDto.IsComplete;

        context.Todos.Update(todo);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int todoId)
    {
        var todo = await context.Todos.FindAsync(todoId);
        if (todo == null) return false;

        context.Todos.Remove(todo);
        await context.SaveChangesAsync();
        return true;
    }
}