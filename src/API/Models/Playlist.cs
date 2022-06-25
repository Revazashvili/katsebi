namespace API.Models;

public record Playlist(int Id,string Name,IEnumerable<Episode> Episodes);