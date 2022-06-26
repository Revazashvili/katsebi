namespace API.Models;

public record PlaylistResponse(int Id, string Name)
{
    public static PlaylistResponse Empty => new(0, string.Empty);
}