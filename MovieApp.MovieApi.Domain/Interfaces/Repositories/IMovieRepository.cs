using MovieApp.Domain.Entities;

namespace MovieApp.MovieApi.Domain.Interfaces.Repositories;
public interface IMovieRepository
{
    Task<Movie> FindByIdAsync(Guid id);
    IQueryable<Movie> FindAll();
    IQueryable<Rating> FindAllRatingsById(Guid id);
}
