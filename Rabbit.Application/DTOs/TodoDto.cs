using AutoMapper;
using Rabbit.Domain.Entities;

namespace Rabbit.Application.DTOs;

[AutoMap(typeof(Todo), ReverseMap = true)]
public class TodoDto
{
    public Guid GuidId { get; set; }
    public string Title { get; set; }
    public bool IsComplete { get; set; }
}