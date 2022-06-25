namespace API.Models;

public record Quote(int Id, string Text, string? Description, string Author, Episode Episode);