using Database.Entities;

namespace API.Models;

public record PlaylistResponse(int Id, string Name)
{
    public PlaylistResponse(Playlist playlist) : this(playlist.Id,playlist.Name) { }
    public static PlaylistResponse Empty => new(0, string.Empty);
}