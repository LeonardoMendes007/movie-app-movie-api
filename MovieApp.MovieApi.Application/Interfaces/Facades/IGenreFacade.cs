using MovieApp.MovieApi.Application.Responses.Summary;

namespace MovieApp.MovieApi.Application.Interfaces.Facades;
public interface IGenreFacade
{
    Task<IList<GenreSummary>> GetAllGenresAsync();
}
