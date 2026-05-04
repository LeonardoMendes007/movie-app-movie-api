using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Application.Mapper.AutoMapperConfig;
using MovieApp.Infra.Data.DependencyInjection;
using MovieApp.MovieApi.Application.Facade;
using MovieApp.MovieApi.Application.Interfaces.Facades;
using MovieApp.MovieApi.Application.Interfaces.Services;
using MovieApp.MovieApi.Application.Services;
using MovieApp.MovieApi.Domain.Interfaces.Repositories;
using MovieApp.MovieApi.Domain.Interfaces.Services;
using MovieApp.MovieApi.Infra.Cache;
using MovieApp.MovieApi.Infra.Persistence.Repositories;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        #region Banco de dados
        var connectionString = Environment.GetEnvironmentVariable("MOVIE_READ_CONNECTION") ?? configuration.GetConnectionString("MovieDbReadOnlyConnection");
        services.AddMovieAppReadDbContext(connectionString);
        #endregion

        #region Cache
        var cacheConnection = Environment.GetEnvironmentVariable("REDIS_CONNECTION") ?? configuration.GetConnectionString("CacheConnection");
        var cachingSettings = configuration.GetSection("CachingSettings");
        var absoluteExpirationRelativeToNow = Environment.GetEnvironmentVariable("ABSOLUTE_EXPIRATION_RELATIVE") ?? cachingSettings.GetValue<string>("AbsoluteExpirationRelativeToNow");
        var slidingExpiration = Environment.GetEnvironmentVariable("SLIDING_EXPIRATION") ?? cachingSettings.GetValue<string>("SlidingExpiration");

        services.AddStackExchangeRedisCache(o =>
        {
            o.Configuration = cacheConnection;
        });

        var cacheEntryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(int.Parse(absoluteExpirationRelativeToNow)), 
            SlidingExpiration = TimeSpan.FromMinutes(int.Parse(slidingExpiration)),
            
        };

        services.AddSingleton<DistributedCacheEntryOptions>(cacheEntryOptions);
        #endregion

        #region AutoMapper
        services.AddAutoMapper(typeof(AutoMapperConfiguration));
        #endregion

        #region Repositories
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        #endregion

        #region Services
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<ICacheService, CacheService>();
        #endregion

        #region Facade
        services.AddScoped<IMovieFacade, MovieFacade>();
        services.AddScoped<IGenreFacade, GenreFacade>();
        #endregion

        #region Telemetry

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("MovieApp.MovieApi"))
            .WithTracing(tracing =>
            {

                tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSqlClientInstrumentation(o => o.SetDbStatementForText = true);
                
                tracing.AddOtlpExporter();
            });

        #endregion

        return services;
    }
}
