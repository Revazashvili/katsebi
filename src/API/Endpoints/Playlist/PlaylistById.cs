using API.Models;
using Ardalis.ApiEndpoints;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Playlist;

[Route("api/playlist")]
public class PlaylistById : EndpointBaseAsync
    .WithRequest<int>
    .WithResult<PlaylistResponse?>
{
    private readonly KatsebiContext _context;

    public PlaylistById(KatsebiContext context) => _context = context;

    [HttpGet,SwaggerOperation(Description = "Returns playlist by id",
         OperationId = "Playlist.ById",
         Tags = new []{"Playlist"})]
    public override async Task<PlaylistResponse?> HandleAsync([FromQuery] int id,
        CancellationToken cancellationToken = new())
    {
        var playlist = await _context.Playlists.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        return playlist is null ? PlaylistResponse.Empty : new PlaylistResponse(playlist.Id, playlist.Name);
    }
}