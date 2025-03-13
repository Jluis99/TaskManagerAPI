using Microsoft.AspNetCore.Mvc;
using MiApiMySQL.Data;
using MiApiMySQL.Models;
using Microsoft.EntityFrameworkCore;

namespace MiApiMySQL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly AppDbContext _context;

    public TaskController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/task
    [HttpGet]
    public async System.Threading.Tasks.Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
    {
        return await _context.Tasks.ToListAsync();
    }

    // GET: api/task/1
    [HttpGet("{id}")]
    public async System.Threading.Tasks.Task<ActionResult<TaskItem>> GetOne(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();
        return task;
    }

    // POST: api/task
    [HttpPost]
    public async System.Threading.Tasks.Task<ActionResult<TaskItem>> Save(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOne), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem updatedTask)
    {
        if (id != updatedTask.Id)
        {
            return BadRequest("El ID en la URL no coincide con el ID en el cuerpo de la solicitud.");
        }

        var existingTask = await _context.Tasks.FindAsync(id);
        if (existingTask == null)
        {
            return NotFound($"No se encontró la tarea con ID {id}");
        }

        // Actualizar propiedades
        existingTask.Title = updatedTask.Title;
        existingTask.Description = updatedTask.Description;
        existingTask.State = updatedTask.State;

        _context.Tasks.Update(existingTask);
        await _context.SaveChangesAsync();

        return Ok(existingTask);
    }

    // DELETE: api/task/1
    [HttpDelete("{id}")]
    public async System.Threading.Tasks.Task<IActionResult> Delete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
