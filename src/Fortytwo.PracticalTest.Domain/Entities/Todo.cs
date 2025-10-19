namespace Fortytwo.PracticalTest.Domain.Entities;

public class Todo : BaseEntity
{
    public string Title { get; set; }
    
    public string? Description { get; set; }
    
    public bool Done { get; set; }
    
    public DateTime? DueDate { get; set; }
    
    public User? Assignee { get; set; }
    public int? AssigneeId { get; set; }
    
    public User Author { get; set; }
}