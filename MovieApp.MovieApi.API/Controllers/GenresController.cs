using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.MovieApi.Application.Interfaces;
using MovieApp.MovieApi.Application.Interfaces.Facades;

namespace MovieApp.MovieApi.API.Controllers;
[Route("api/genres")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGenreFacade _genreFacade;
    private readonly ILogger<GenresController> _logger;

    public GenresController(IGenreFacade genreFacade, ILogger<GenresController> logger)
    {
        _genreFacade = genreFacade;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        return Ok(await _genreFacade.GetAllGenresAsync());
    }
}
