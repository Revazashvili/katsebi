using API.Models;
using Ardalis.ApiEndpoints;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.Episode;

[Route("api/episode")]
public class EpisodeById : EndpointBaseAsync
    .WithRequest<int>
    .WithActionResult<EpisodeResponse?>
{
    private readonly KatsebiContext _context;

    public EpisodeById(KatsebiContext context) => _context = context;

    [HttpGet,SwaggerOperation(Description = "Returns episode by id",
         OperationId = "Episode.ById",
         Tags = new []{"Episode"}),
     SwaggerResponse(StatusCodes.Status200OK,Type = typeof(EpisodeResponse)),
     SwaggerResponse(StatusCodes.Status204NoContent,Description = "no episode can be found with passed id")]
    public override async Task<ActionResult<EpisodeResponse?>> HandleAsync([FromQuery]int id, CancellationToken cancellationToken = new())
    {
        var playlist = await _context.Episodes.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        return playlist is null ? NoContent() : Ok(new EpisodeResponse(playlist));
    }
}