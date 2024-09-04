using Microsoft.AspNetCore.Mvc;
using MovieApp.MovieApi.API.Query;
using MovieApp.MovieApi.API.QueryParams;
using MovieApp.MovieApi.API.Response;
using MovieApp.MovieApi.Application.Interfaces.Facades;
using MovieApp.MovieApi.Application.Pagination.Interface;
using MovieApp.MovieApi.Application.Queries.Movie;
using MovieApp.MovieApi.Application.Queries.Rating;
using MovieApp.MovieApi.Application.Responses.Details;
using MovieApp.MovieApi.Application.Responses.Summary;
using System.Net;

namespace MovieApp.MovieApi.API.Controllers;
[Route("api/movies")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieFacade _movieFacade;
    private readonly ILogger<MovieController> _logger;

    public MovieController(IMovieFacade movieFacade, ILogger<MovieController> logger)
    {
        _movieFacade = movieFacade;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var movie = await _movieFacade.GetMovieById(id);

        return Ok(ResponseBase<MovieDetails>.ResponseBaseFactory(movie, System.Net.HttpStatusCode.OK));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetMoviesQueryParams getMoviesQueryParams)
    {

        var getMoviesQuery = new GetMoviesQuery()
        {
            GenreId = getMoviesQueryParams.GenreId,
            SearchTerm = getMoviesQueryParams.SearchTerm,
            ReleaseYear = getMoviesQueryParams.ReleaseYear,
            Sort = getMoviesQueryParams.Sort.Split(",").ToList(),
            Page = getMoviesQueryParams.Page,
            PageSize = getMoviesQueryParams.PageSize
        };

        var movies = await _movieFacade.GetMoviesByQuery(getMoviesQuery);

        return Ok(ResponseBase<IPagedList<MovieSummary>>.ResponseBaseFactory(movies, HttpStatusCode.OK));
    }

    [HttpGet("{id}/ratings")]
    public async Task<IActionResult> GetRatings([FromRoute] Guid id, [FromQuery] GetRatingByMovieQueryParams getRatingByMovieQueryParams)
    {

        var getRatingByMovieQuery = new GetRatingByMovieQuery()
        {
            MovieId = id,
            Sort = getRatingByMovieQueryParams.Sort.Split(",").ToList(),
            Page = getRatingByMovieQueryParams.Page,
            PageSize = getRatingByMovieQueryParams.PageSize
        };

        var ratings = await _movieFacade.GetRatingByMovieId(getRatingByMovieQuery);

        return Ok(ResponseBase<IPagedList<RatingSummary>>.ResponseBaseFactory(ratings, HttpStatusCode.OK));
    }
}
