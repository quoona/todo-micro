using Microsoft.AspNetCore.Mvc;
using Rabbit.Application.DTOs;
using Rabbit.Application.Interfaces;

namespace Rabbit.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController(ITodoService todoService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await todoService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await todoService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TodoDto dto)
    {
        var result = await todoService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] TodoDto dto)
    {
        var result = await todoService.UpdateAsync(id, dto);
        return Ok(result);
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await todoService.DeleteAsync(id);
        return Ok(result);
    }
}