using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Entities;
using MovieApp.Infra.Data;
using MovieApp.MovieApi.Domain.Interfaces.Repositories;

namespace MovieApp.MovieApi.Infra.Persistence.Repositories;
public class MovieRepository : IMovieRepository
{
    private readonly MovieAppReadDbContext _context;
    public MovieRepository(MovieAppReadDbContext context)
    {
        _context = context;
    }

    public IQueryable<Movie> FindAll()
    {
        return _context.Movies.AsQueryable();
    }

    public IQueryable<Rating> FindAllRatingsById(Guid id)
    {
        return _context.Ratings.Where(r => r.MovieId == id).AsQueryable();
    }

    public async Task<Movie> FindByIdAsync(Guid id)
    {
        return await _context.Movies.Include(m => m.Genries).FirstAsync(m => m.Id == id);
    }

}
