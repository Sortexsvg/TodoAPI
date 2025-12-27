using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {

        private readonly ITodoService _service;
        private readonly IMapper _mapper;


        public TodoController(ITodoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var todo = await _service.GetByIdAsync(id);
            if (todo == null)
                return NotFound();

            return Ok(_mapper.Map<TodoResponseDto>(todo));
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(CreateTodoDto dto)
        {
            var todo = _mapper.Map<TodoItem>(dto);

            var created = await _service.AddAsync(todo);

            var response = _mapper.Map<TodoResponseDto>(created);

            return CreatedAtAction(
                nameof(GetTodoById),
                new { id = response.Id },
                response
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoItem todo)
        {
            var updated = await _service.UpdateAsync(id, todo);
            if (!updated)
                return NotFound();

            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedTodos(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var todos = await _service.GetPagedAsync(page, pageSize);

            var response = todos.Select(t => new TodoResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                IsCompleted = t.IsCompleted
            });

            return Ok(response);
        }
    }
}
