using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITodoService
    {
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(int id);
        Task<TodoItem> AddAsync(TodoItem item);
        Task<bool> UpdateAsync(int id, TodoItem item);
        Task<bool> DeleteAsync(int id);
        Task<List<TodoItem>> GetPagedAsync(int page, int pageSize);

    }
}
