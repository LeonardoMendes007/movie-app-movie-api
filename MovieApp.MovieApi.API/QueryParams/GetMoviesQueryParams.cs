using System.ComponentModel.DataAnnotations;

namespace MovieApp.MovieApi.API.Query;

public class GetMoviesQueryParams : PagedListQueryParams
{
    public Guid? GenreId { get; set; } = null;
    public string SearchTerm { get; set; } = string.Empty;
    public int? ReleaseYear{ get; set;} = null;
    public string Sort { get; set; } = string.Empty;

}
