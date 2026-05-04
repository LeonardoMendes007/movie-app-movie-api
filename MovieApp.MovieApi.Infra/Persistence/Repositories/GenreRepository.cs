using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Entities;
using MovieApp.Infra.Data;
using MovieApp.MovieApi.Domain.Interfaces.Repositories;

namespace MovieApp.MovieApi.Infra.Persistence.Repositories;
public class GenreRepository : IGenreRepository
{
    private readonly MovieAppReadDbContext _context;
    public GenreRepository(MovieAppReadDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Genre>> FindAllAsync()
    {
        return await _context.Genres.ToListAsync();
    }
}
