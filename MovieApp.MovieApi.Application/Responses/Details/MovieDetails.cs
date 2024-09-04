using MovieApp.MovieApi.Application.Responses.Summary;

namespace MovieApp.MovieApi.Application.Responses.Details;
public class MovieDetails
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Synopsis { get; set; }
    public string ImageUrl { get; set; }
    public string PathM3U8File { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int Views { get; set; }
    public IEnumerable<GenreSummary> Genries { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; }
}
