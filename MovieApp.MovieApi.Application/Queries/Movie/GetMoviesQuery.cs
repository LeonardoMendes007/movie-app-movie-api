namespace MovieApp.MovieApi.Application.Queries.Movie;
public class GetMoviesQuery : PagedListQuery
{
    public Guid? GenreId { get; set; }
    public string SearchTerm { get; set; }
    public int? ReleaseYear { get; set; }
    public List<string>? Sort { get; set; }

    public string getCacheKey()
    {
        return (GenreId + SearchTerm + ReleaseYear + string.Join("",Sort) + Page + PageSize).ToLower();
    }
}
