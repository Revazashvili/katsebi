using Database.Entities;

namespace API.Models;

public record EpisodeResponse(int Id, string Name, int? EpisodeNumber, string YoutubeUrl, DateOnly UploadTime,
    PlaylistResponse PlaylistResponse, IEnumerable<GuestResponse> Guests)
{
    public EpisodeResponse(Episode episode) : this(episode.Id,episode.Name,episode.EpisodeNumber,episode.YoutubeUrl,episode.UploadTime,
        new PlaylistResponse(episode.Playlist),episode.Guests.Select(guest => new GuestResponse(guest.Id,guest.Name,guest.Description))) {}
}