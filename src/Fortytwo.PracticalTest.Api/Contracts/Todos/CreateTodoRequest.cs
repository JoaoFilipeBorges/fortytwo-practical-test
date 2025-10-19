using System.ComponentModel.DataAnnotations;

namespace Fortytwo.PracticalTest.Api.Contracts.Todos;

public class CreateTodoRequest
{
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int? Assignee { get; set; }
    public bool Done { get; set; }
}