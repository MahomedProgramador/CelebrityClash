
using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public partial class AppUser : IdentityUser<int>
{
    public DateOnly DateOfBirth { get; set;}
    public required string KnownAs { get; set;}
    public DateTime Created { get; set;} = DateTime.UtcNow;
    public DateTime LastActive { get; set;} = DateTime.UtcNow;
    public required string Gender { get; set; }
    public string? Introduction { get; set; }
    public string? LookingFor { get; set; }
    public string? Interests { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public List<Photo> Photos { get; set; } = []; // isto é uma navigation property o EF vai tratar das relações
    public List<UserDislike> DislikedByUsers { get; set; } = []; 
    public List<UserDislike> DislikedUsers { get; set; } = [];
    public List<Message> MessagesSent { get; set; } = [];
    public List<Message> MessagesReceived { get; set; } = [];
    public ICollection<AppUserRole> UserRoles { get; set; } = [];
}