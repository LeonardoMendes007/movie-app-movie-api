using MovieApp.Domain.Entities;

namespace MovieApp.MovieApi.Application.Extensions;
public static class ExtensionsMethods
{
    public static IQueryable<Movie> OrderBy(this IQueryable<Movie> source, List<string> sortQueryList)
    {
        IOrderedQueryable<Movie>? orderable = null;
        foreach (var item in sortQueryList)
        {
            if (string.IsNullOrEmpty(item)) continue;

            if (item.StartsWith("-"))
            {
                orderable = item[1..].Trim().ToLower() switch
                {
                    "name" => orderable is null ? source.OrderByDescending(m => m.Name) : orderable.ThenByDescending(m => m.Name),
                    "releasedate" => orderable is null ? source.OrderByDescending(m => m.ReleaseDate) : orderable.ThenByDescending(m => m.ReleaseDate),
                    "views" => orderable is null ? source.OrderByDescending(m => m.Views) : orderable.ThenByDescending(m => m.Views),
                    "rating" => orderable is null ? source.OrderByDescending(m => m.Ratings.Average(r => r.Score)) : orderable.ThenByDescending(m => m.Ratings.Average(r => r.Score)),
                    _ => orderable
                };
            }
            else
            {
                orderable = item.Trim().ToLower() switch
                {
                    "name" => orderable is null ? source.OrderBy(m => m.Name) : orderable.ThenBy(m => m.Name),
                    "releasedate" => orderable is null ? source.OrderBy(m => m.ReleaseDate) : orderable.ThenBy(m => m.ReleaseDate),
                    "views" => orderable is null ? source.OrderBy(m => m.Views) : orderable.ThenBy(m => m.Views),
                    "rating" => orderable is null ? source.OrderBy(m => m.Ratings.Average(r => r.Score)) : orderable.ThenBy(m => m.Ratings.Average(r => r.Score)),
                    _ => orderable
                };
            }
        }

        return orderable is not null ? orderable : source;
    }


    public static IQueryable<Rating> OrderBy(this IQueryable<Rating> source, List<string> sortQueryList)
    {
        IOrderedQueryable<Rating>? orderable = null;
        foreach (var item in sortQueryList)
        {
            if (string.IsNullOrEmpty(item)) continue;

            if (item.StartsWith("-"))
            {
                orderable = item[1..].Trim().ToLower() switch
                {
                    "score" => orderable is null ? source.OrderByDescending(m => m.Score) : orderable.ThenByDescending(m => m.Score),
                    _ => orderable
                };
            }
            else
            {
                orderable = item.Trim().ToLower() switch
                {
                    "score" => orderable is null ? source.OrderBy(m => m.Score) : orderable.ThenBy(m => m.Score),
                    _ => orderable
                };
            }
        }

        return orderable is not null ? orderable : source;
    }
}
