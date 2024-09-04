using MovieApp.MovieApi.Application.Pagination;
using MovieApp.MovieApi.Application.Pagination.Interface;
using MovieApp.MovieApi.Application.Queries.Movie;
using MovieApp.MovieApi.Application.Queries.Rating;
using MovieApp.MovieApi.Application.Responses.Details;
using MovieApp.MovieApi.Application.Responses.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.MovieApi.Application.Interfaces.Services;
public interface IMovieService
{
    Task<MovieDetails> GetMovieByIdAsync(Guid id);
    Task<IPagedList<MovieSummary>> GetMoviesByQuery(GetMoviesQuery query);
    Task<IPagedList<RatingSummary>> GetRatingByQuery(GetRatingByMovieQuery request);
    Task IncrementViewsAsync(Guid id);
}
