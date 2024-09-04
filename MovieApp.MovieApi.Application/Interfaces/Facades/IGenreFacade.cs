using MovieApp.MovieApi.Application.Pagination.Interface;
using MovieApp.MovieApi.Application.Queries.Movie;
using MovieApp.MovieApi.Application.Queries.Rating;
using MovieApp.MovieApi.Application.Responses.Details;
using MovieApp.MovieApi.Application.Responses.Summary;

namespace MovieApp.MovieApi.Application.Interfaces.Facades;
public interface IGenreFacade
{
    Task<IList<GenreSummary>> GetAllGenresAsync();
}
