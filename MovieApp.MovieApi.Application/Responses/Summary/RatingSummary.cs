namespace MovieApp.MovieApi.Application.Responses.Summary;
public class RatingSummary
{
    public Guid ProfileId { get; set; }
    public string UserName { get; set; }
    public int Score { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
