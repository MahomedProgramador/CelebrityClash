using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("Photos")]
public class Photo
{
    public int Id { get; set;}
    public required string Url { get; set; }
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }

    //A way to enforce a required propery in a one to many
    //Navigation properties
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!; //null forgiving
}

