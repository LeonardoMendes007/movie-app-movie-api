using Microsoft.EntityFrameworkCore;
using MovieApp.Domain.Entities;
using MovieApp.Infra.Data;
using MovieApp.MovieApi.Domain.Interfaces.Repositories;

namespace MovieApp.MovieApi.Infra.Persistence.Repositories;
public class GenreRepository : IGenreRepository
{
    private readonly MovieAppDbContext _context;
    public GenreRepository(MovieAppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Genre>> FindAllAsync()
    {
        return await _context.Genres.ToListAsync();
    }
}
