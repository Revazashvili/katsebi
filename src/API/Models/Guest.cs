namespace API.Models;

public record Guest(int Id, string Name, string? Description, IEnumerable<Episode> Episodes);