using AutoMapper;
using MovieApp.MovieApi.Application.Interfaces.Services;
using MovieApp.MovieApi.Application.Responses.Summary;
using MovieApp.MovieApi.Domain.Interfaces.UnitOfWork;

namespace MovieApp.MovieApi.Application.Services;
public class GenreService : IGenreService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IList<GenreSummary>> GetAllGenresAsync()
    {
        var genres = await _unitOfWork.GenreRepository.FindAllAsync();
        return _mapper.Map<List<GenreSummary>>(genres);
    }
}
