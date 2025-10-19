namespace Fortytwo.PracticalTest.Application.ReadModel;

public class TodoDto
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public bool Done { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public UserDto Assignee { get; set; }
    
    public UserDto Creator { get; set; }
    
    public string ExternalTitle { get; set; }
}