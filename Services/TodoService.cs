using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext _context;
        ILogger<TodoService> _logger;
        public TodoService(AppDbContext context, ILogger<TodoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task<TodoItem> AddAsync(TodoItem item)
        {
            _context.Todos.Add(item);
            await _context.SaveChangesAsync();
            _logger.Log(LogLevel.Debug, "Added");
            return item;
        }

        public async Task<bool> UpdateAsync(int id, TodoItem item)
        {
            var existing = await GetByIdAsync(id);
            if (existing == null)
                return false;

            existing.Title = item.Title;
            existing.IsCompleted = item.IsCompleted;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todo = await GetByIdAsync(id);
            if (todo == null)
                return false;

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TodoItem>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Todos
                .OrderBy(t => t.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
