using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs
{
    public class CreateTodoDto
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; } = string.Empty;
    }
}
