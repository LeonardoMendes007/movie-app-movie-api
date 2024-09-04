namespace MovieApp.MovieApi.Application.Responses.Summary;
public class MovieSummary
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Synopsis { get; set; }
    public string ImageUrl { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int Views { get; set; }
}
