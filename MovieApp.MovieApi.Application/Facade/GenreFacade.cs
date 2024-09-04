using MovieApp.MovieApi.Application.Interfaces.Facades;
using MovieApp.MovieApi.Application.Interfaces.Services;
using MovieApp.MovieApi.Application.Queries.Movie;
using MovieApp.MovieApi.Application.Responses.Summary;
using MovieApp.MovieApi.Domain.Interfaces.Services;

namespace MovieApp.MovieApi.Application.Facade;
public class GenreFacade : IGenreFacade
{
    private readonly ICacheService _cachingService;
    private readonly IGenreService _genreService;

    public GenreFacade(ICacheService cachingService, IGenreService genreService)
    {
        _cachingService = cachingService;
        _genreService = genreService;
    }

    public async Task<IList<GenreSummary>> GetAllGenresAsync()
    {
        var genresSummaryCache = await _cachingService.GetAsync<List<GenreSummary>>("Genres");

        if(genresSummaryCache is not null) return genresSummaryCache;

        var genresSummary = await _genreService.GetAllGenresAsync();

        await _cachingService.SetAsync("Genres", genresSummary);

        return genresSummary;
    }
}
