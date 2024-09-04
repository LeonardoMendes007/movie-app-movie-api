using AutoMapper;
using MovieApp.MovieApi.Application.Extensions;
using MovieApp.MovieApi.Application.Interfaces;
using MovieApp.MovieApi.Application.Pagination;
using MovieApp.MovieApi.Application.Pagination.Interface;
using MovieApp.MovieApi.Application.Queries.Movie;
using MovieApp.MovieApi.Application.Queries.Rating;
using MovieApp.MovieApi.Application.Responses.Details;
using MovieApp.MovieApi.Application.Responses.Summary;
using MovieApp.MovieApi.Domain.Interfaces.UnitOfWork;

namespace MovieApp.MovieApi.Application.Services;
public class MovieService : IMovieService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MovieDetails> GetMovieByIdAsync(Guid id)
    {
        var movie = await _unitOfWork.MovieRepository.FindByIdAsync(id);

        return _mapper.Map<MovieDetails>(movie);    
    }

    public async Task<IPagedList<MovieSummary>> GetMoviesByQuery(GetMoviesQuery query)
    {
        var moviesQuery = _unitOfWork.MovieRepository.FindAll();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            moviesQuery = moviesQuery.Where(m => m.Name.ToLower().Contains(query.SearchTerm.ToLower()) || m.Synopsis.ToLower().Contains(query.SearchTerm.ToLower()));
        }

        if (query.GenreId is not null)
        {
            moviesQuery = moviesQuery.Where(m => m.Genries.Any(x => x.Id.ToString() == query.GenreId.ToString()));
        }

        if(query.ReleaseYear is not null)
        {
            moviesQuery = moviesQuery.Where(m => m.ReleaseDate.Year == query.ReleaseYear);
        }

        if (query.Sort.Any())
        {
            moviesQuery = moviesQuery.OrderBy(query.Sort);
        }

        var moviesSummaryQuery = moviesQuery
            .Select(m => new MovieSummary
            {
                Id = m.Id,
                Name = m.Name,
                Synopsis = m.Synopsis,
                ImageUrl = m.ImageUrl,
                ReleaseDate = m.ReleaseDate,
                Views = m.Views
            });

        var pagedListMovie = PagedList<MovieSummary>.CreatePagedList(moviesSummaryQuery, query.Page, query.PageSize);

        return pagedListMovie;
    }

    public async Task<IPagedList<RatingSummary>> GetRatingByQuery(GetRatingByMovieQuery query)
    {
        var ratingsQuery = _unitOfWork.MovieRepository.FindAllRatingsById(query.MovieId);

        if (query.Sort.Any())
        {
            ratingsQuery = ratingsQuery.OrderBy(query.Sort);
        }

        var ratingsSummaryQuery = ratingsQuery
            .Select(m => new RatingSummary
            {
                ProfileId = m.ProfileId,
                UserName = m.Profile.UserName,
                Comment = m.Comment,
                Score = m.Score,
                CreatedDate = m.CreatedDate,
                UpdatedDate = m.UpdatedDate
            });

        var pagedListRating = PagedList<RatingSummary>.CreatePagedList(ratingsSummaryQuery, query.Page, query.PageSize);

        return pagedListRating;
    }

    public async Task IncrementViewsAsync(Guid id)
    {
        var movie = await _unitOfWork.MovieRepository.FindByIdAsync(id);
        movie.Views++;

        await _unitOfWork.CommitAsync();
    }
}
