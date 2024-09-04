using MovieApp.MovieApi.Application.Interfaces.Facades;
using MovieApp.MovieApi.Application.Interfaces.Services;
using MovieApp.MovieApi.Application.Pagination;
using MovieApp.MovieApi.Application.Pagination.Interface;
using MovieApp.MovieApi.Application.Queries.Movie;
using MovieApp.MovieApi.Application.Queries.Rating;
using MovieApp.MovieApi.Application.Responses.Details;
using MovieApp.MovieApi.Application.Responses.Summary;
using MovieApp.MovieApi.Domain.Interfaces.Services;

namespace MovieApp.MovieApi.Application.Facade;
public class MovieFacade : IMovieFacade
{
    private readonly ICacheService _cachingService;
    private readonly IMovieService _movieService;

    public MovieFacade(ICacheService cachingService, IMovieService movieService)
    {
        _cachingService = cachingService;
        _movieService = movieService;
    }

    public async Task<MovieDetails> GetMovieById(Guid id)
    {
        var movieCache = await _cachingService.GetAsync<MovieDetails>($"Movie_{id.ToString()}");

        if (movieCache is not null)
        {
            await _movieService.IncrementViewsAsync(id);
            return movieCache;
        }

        var movieDetails = await _movieService.GetMovieByIdAsync(id);

        if (movieDetails is null)
        {
            throw new Exception();
        }

        _ = _movieService.IncrementViewsAsync(id);

        await _cachingService.SetAsync($"Movie_{id.ToString()}", movieDetails);

        return movieDetails;
    }
    public async Task<IPagedList<MovieSummary>> GetMoviesByQuery(GetMoviesQuery query)
    {
        var movieCache = await _cachingService.GetAsync<PagedList<MovieSummary>>($"Movie_{query.getCacheKey()}");

        if (movieCache is not null) return movieCache;

        var pagedMovieSummary = await _movieService.GetMoviesByQuery(query);

        await _cachingService.SetAsync($"Movie_{query.getCacheKey()}", pagedMovieSummary);

        return pagedMovieSummary;
    }
    public async Task<IPagedList<RatingSummary>> GetRatingByMovieId(GetRatingByMovieQuery query)
    {
        var ratingCache = await _cachingService.GetAsync<PagedList<RatingSummary>>($"Rating_{query.getCacheKey()}");
        if (ratingCache is not null) return ratingCache;

        var pagedRatingSummary = await _movieService.GetRatingByQuery(query);

        await _cachingService.SetAsync($"Rating_{query.getCacheKey()}", pagedRatingSummary);

        return pagedRatingSummary;
    }
}
