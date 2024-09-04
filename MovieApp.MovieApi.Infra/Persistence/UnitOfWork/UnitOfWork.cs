using MovieApp.Infra.Data;
using MovieApp.MovieApi.Domain.Interfaces.Repositories;
using MovieApp.MovieApi.Domain.Interfaces.UnitOfWork;
using MovieApp.MovieApi.Infra.Persistence.Repositories;

namespace MovieApp.MovieApi.Infra.Persistence.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private IMovieRepository _movieRepository;
    private IGenreRepository _genreRepository;

    private readonly MovieAppDbContext _movieAppDbContext;

    public UnitOfWork(MovieAppDbContext movieAppDbContext)
    {
        _movieAppDbContext = movieAppDbContext;
    }

    public IMovieRepository MovieRepository
    {
        get
        {
            return _movieRepository = _movieRepository ?? new MovieRepository(_movieAppDbContext);
        }
    }

    public IGenreRepository GenreRepository
    {
        get
        {
            return _genreRepository = _genreRepository ?? new GenreRepository(_movieAppDbContext);
        }
    }

    public async Task CommitAsync()
    {
        await _movieAppDbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _movieAppDbContext.Dispose();
    }
}
