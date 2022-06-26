using API.Models;
using Ardalis.ApiEndpoints;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Playlist;

[Route("api/playlists")]
public class Playlists : EndpointBaseAsync
    .WithoutRequest
    .WithResult<IEnumerable<PlaylistResponse>>
{
    private readonly KatsebiContext _context;

    public Playlists(KatsebiContext context) => _context = context;

    [HttpGet,SwaggerOperation(Description = "Returns all playlist",
         OperationId = "Playlist.All",
         Tags = new []{"Playlist"})]
    public override async Task<IEnumerable<PlaylistResponse>>
        HandleAsync(CancellationToken cancellationToken = new()) =>
        await _context.Playlists.Select(playlist => new PlaylistResponse(playlist.Id, playlist.Name))
            .ToListAsync(cancellationToken);
}