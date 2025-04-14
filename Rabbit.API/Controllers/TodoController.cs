using Microsoft.AspNetCore.Mvc;
using Rabbit.Application.DTOs;
using Rabbit.Application.Interfaces;

namespace Rabbit.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController(ITodoService todoService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await todoService.GetAllAsync());

    [HttpGet("{id}")]
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] TodoDto dto)
    {
        var result = await todoService.UpdateAsync(id, dto);
        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => Ok(await todoService.DeleteAsync(id));
}