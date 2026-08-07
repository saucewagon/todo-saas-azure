using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Dtos;

public class CreateTodoRequest
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = "";
}