using MovieApp.MovieApi.Application.Pagination.Interface;
using MovieApp.MovieApi.Application.Pagination;
using MovieApp.MovieApi.Application.Queries.Movie;
using MovieApp.MovieApi.Application.Queries.Rating;
using MovieApp.MovieApi.Application.Responses.Details;
using MovieApp.MovieApi.Application.Responses.Summary;
using MovieApp.MovieApi.Application.Services;
using MovieApp.MovieApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.MovieApi.Application.Interfaces;
public interface IMovieFacade
{
    Task<MovieDetails> GetMovieById(Guid id);
    Task<IPagedList<MovieSummary>> GetMoviesByQuery(GetMoviesQuery query);
    Task<IPagedList<RatingSummary>> GetRatingByMovieId(GetRatingByMovieQuery query);
}
