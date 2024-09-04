using MovieApp.MovieApi.Application.Pagination.Interface;
using MovieApp.MovieApi.Application.Responses.Summary;

namespace MovieApp.MovieApi.Application.Queries.Rating;
public class GetRatingByMovieQuery : PagedListQuery
{
    public Guid MovieId { get; set; }
    public List<string> Sort { get; set; }

    public string getCacheKey()
    {
        return (MovieId + string.Join("", Sort) + Page + PageSize).ToLower();
    }
}
