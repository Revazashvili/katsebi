namespace API.Persistence.Entities;

#pragma warning disable CS8618
public class Episode
{
    public Episode()
    {
        Guests = new HashSet<Guest>();
        Quotes = new HashSet<Quote>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int? EpisodeNumber { get; set; }
    public string YoutubeUrl { get; set; }
    public DateTime UploadTime { get; set; }
    public int PlaylistId { get; set; }
    public Playlist Playlist { get; set; }
    public ICollection<Guest> Guests { get; set; }
    public ICollection<Quote> Quotes { get; set; }
}