using MovieApp.MovieApi.Domain.Interfaces.Repositories;

namespace MovieApp.MovieApi.Domain.Interfaces.UnitOfWork;
public interface IUnitOfWork
{
    IMovieRepository MovieRepository { get; }
    IGenreRepository GenreRepository { get; }
    Task CommitAsync();
}
