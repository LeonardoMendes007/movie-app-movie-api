using AutoMapper;
using MovieApp.MovieApi.Application.Mapper;

namespace MovieApp.Application.Mapper.AutoMapperConfig;
public static class AutoMapperConfiguration
{
    public static MapperConfiguration RegisterMappings()
        => new(mc =>
        {
            mc.AddProfiles(new List<Profile>(){new DomainToResponse()});
        });
}
