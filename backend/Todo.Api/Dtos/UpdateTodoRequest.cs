using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Dtos;

public class UpdateTodoRequest
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = "";

    public bool Completed { get; set; }
}