using MovieApp.Domain.Entities;

namespace MovieApp.MovieApi.Domain.Interfaces.Repositories;
public interface IGenreRepository
{
    Task<IEnumerable<Genre>> FindAllAsync();
}
