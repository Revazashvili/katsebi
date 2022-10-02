namespace Database.Entities;

#pragma warning disable CS8618
public class Quote
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string? Description { get; set; }
    public string Author { get; set; }
    public int EpisodeId { get; set; }
    public Episode Episode { get; set; }
}