using System.ComponentModel.DataAnnotations.Schema;

namespace Fortytwo.PracticalTest.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }
    
    public string ProfilePicUrl { get; set; }
    
    public string PhoneNumber { get; set; }
    
    [NotMapped] public string Password { get; set; }
}