using API.Models;
using Ardalis.ApiEndpoints;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Episode;

[Route("api/episodes")]
public class Episodes : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<IEnumerable<EpisodeResponse>>
{
    private readonly KatsebiContext _context;

    public Episodes(KatsebiContext context) => _context = context;

    [HttpGet, SwaggerOperation(Description = "Returns all episodes",
         OperationId = "Episode.All",
         Tags = new[] { "Episode" }),
     SwaggerResponse(StatusCodes.Status200OK,Type = typeof(IEnumerable<EpisodeResponse>)),
     SwaggerResponse(StatusCodes.Status204NoContent,Description = "no episodes can be found")]
    public override async Task<ActionResult<IEnumerable<EpisodeResponse>>> HandleAsync(CancellationToken cancellationToken = new())
    {
        var episodes = await _context.Episodes
            .Include(episode => episode.Playlist)
            .Include(episode => episode.Guests)
            .Select(episode => new EpisodeResponse(episode))
            .ToListAsync(cancellationToken);
        return episodes.Any() ? Ok(episodes) : NoContent();

    }
}