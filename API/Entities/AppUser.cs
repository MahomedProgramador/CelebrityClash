
using API.Extensions;

namespace API.Entities;

public partial class AppUser
{
    public int Id { get; set; } 
    public required string UserName { get; set; }
    public byte[] PasswordHash{get; set;}  = [];
    public byte[] PasswordSalt{get; set;} = [];
    public DateOnly DateOfBirth { get; set;}
    public required string KnownAs { get; set;}
    public DateTime Created { get; set;} = DateTime.UtcNow;
    public DateTime LastActive { get; set;} = DateTime.UtcNow;
    public required string Gender { get; set; }
    public string? Introduction { get; set; }
    public string? LoogingFor { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public List<Photo> Photos { get; set; } = []; // isto é uma navigation property o EF vai tratar das relações

    // public int GetAge()
    // {
    //     return DateOfBirth.CalculateAge();
    // }

}