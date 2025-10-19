using Fortytwo.PracticalTest.Domain.Entities;

namespace Fortytwo.PracticalTest.Application.ReadModel.Mappings;

public static class TodoMapping
{
    public static TodoDto ToDto(this Todo todo)
    {
        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            DueDate = todo.DueDate,
            Done = todo.Done,
            Assignee = new UserDto
            {
                Id = todo.AssigneeId,
                UserName = todo.Assignee.UserName
            },
            Creator = new UserDto
            {
                Id = todo.CreatedBy,
                UserName = todo.Author.UserName
            }
        };
    }
}