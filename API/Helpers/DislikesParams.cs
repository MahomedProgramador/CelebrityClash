namespace API.Helpers;

public class DislikesParams : PaginationParams
{
    public int UserId { get; set; }
    public required string Predicate { get; set; } = "disliked";
}
