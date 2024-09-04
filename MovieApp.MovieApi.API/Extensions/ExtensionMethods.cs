namespace MovieApp.MovieApi.API.Extensions;

public static class ExtensionMethods
{
    public static async Task<byte[]> ToByteArrayAsync(this IFormFile arquivo)
    {
        using (var memoryStream = new MemoryStream())
        {
            await arquivo.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
