using AutoMapper;
using MovieApp.MovieApi.Application.Interfaces.Services;
using MovieApp.MovieApi.Application.Responses.Summary;
using MovieApp.MovieApi.Domain.Interfaces.Repositories;

namespace MovieApp.MovieApi.Application.Services;
public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public GenreService(IGenreRepository genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    public async Task<IList<GenreSummary>> GetAllGenresAsync()
    {
        var genres = await _genreRepository.FindAllAsync();
        return _mapper.Map<List<GenreSummary>>(genres);
    }
}
