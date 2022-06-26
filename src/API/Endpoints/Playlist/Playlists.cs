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
    .WithActionResult<IEnumerable<PlaylistResponse>>
{
    private readonly KatsebiContext _context;

    public Playlists(KatsebiContext context) => _context = context;

    [HttpGet, SwaggerOperation(Description = "Returns all playlist",
         OperationId = "Playlist.All",
         Tags = new[] { "Playlist" }),
     SwaggerResponse(StatusCodes.Status200OK,Type = typeof(IEnumerable<PlaylistResponse>)),
     SwaggerResponse(StatusCodes.Status204NoContent,Description = "no playlists can be found")]
    public override async Task<ActionResult<IEnumerable<PlaylistResponse>>>
        HandleAsync(CancellationToken cancellationToken = new())
    {
        var responses = await _context.Playlists
            .Select(playlist => new PlaylistResponse(playlist.Id, playlist.Name))
            .ToListAsync(cancellationToken);
        return responses.Any() ? Ok(responses) : NoContent();
    }
}