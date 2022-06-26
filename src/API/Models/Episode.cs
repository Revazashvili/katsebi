namespace API.Models;

public record Episode(int Id, string Name, int? EpisodeNumber, string YoutubeUrl, DateOnly UploadTime,
    PlaylistResponse PlaylistResponse, IEnumerable<Guest> Guests, IEnumerable<Quote> Quotes);