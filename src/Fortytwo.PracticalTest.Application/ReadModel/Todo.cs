namespace Fortytwo.PracticalTest.Application.ReadModel;

public class Todo
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public bool Done { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public User Assignee { get; set; }
    
    public User Creator { get; set; }
    
    public string ExternalTitle { get; set; }
}