using MovieApp.Domain.Entities;
using MovieApp.MovieApi.Application.Responses.Details;
using MovieApp.MovieApi.Application.Responses.Summary;

namespace MovieApp.MovieApi.Application.Mapper;
public class DomainToResponse : AutoMapper.Profile
{
    public DomainToResponse()
    {

        #region Movie to MovieDetails

        CreateMap<Movie, MovieDetails>();

        #endregion

        #region Rating to RatingSummary

        CreateMap<Rating, RatingSummary>();

        #endregion

        #region Genre to GenreSummary

        CreateMap<Genre, GenreSummary>();

        #endregion
    }
}
