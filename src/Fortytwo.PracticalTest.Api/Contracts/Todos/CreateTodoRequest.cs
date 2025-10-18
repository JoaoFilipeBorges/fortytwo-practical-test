using System.ComponentModel.DataAnnotations;

namespace Fortytwo.PracticalTest.Api.Contracts.Todos;

public record CreateTodoRequest
(
    [Required] string Title,
    string Description,
    DateTime DueDate,
    int Assignee,
    bool Done
);