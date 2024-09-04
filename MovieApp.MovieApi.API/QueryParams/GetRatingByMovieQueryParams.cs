using MovieApp.MovieApi.API.Query;

namespace MovieApp.MovieApi.API.QueryParams;

public class GetRatingByMovieQueryParams : PagedListQueryParams
{
    public string Sort { get; set; } = string.Empty;
}
