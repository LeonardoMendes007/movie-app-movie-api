FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:8000;http://+:80;
ENV ASPNETCORE_ENVIRONMENT=Development

ENV MOVIE_CONNECTION=$MOVIE_CONNECTION
ENV REDIS_CONNECTION=$REDIS_CONNECTION
ENV ABSOLUTE_EXPIRATION_RELATIVE=$ABSOLUTE_EXPIRATION_RELATIVE
ENV SLIDING_EXPIRATION=$SLIDING_EXPIRATION

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MovieApp.MovieApi.API/MovieApp.MovieApi.API.csproj", "MovieApp.MovieApi.API/"]
COPY ["MovieApp.MovieApi.CrossCutting/MovieApp.MovieApi.CrossCutting.csproj", "MovieApp.MovieApi.CrossCutting/"]
COPY ["MovieApp.MovieApi.Application/MovieApp.MovieApi.Application.csproj", "MovieApp.MovieApi.Application/"]
COPY ["MovieApp.MovieApi.Domain/MovieApp.MovieApi.Domain.csproj", "MovieApp.MovieApi.Domain/"]
COPY ["MovieApp.MovieApi.Infra/MovieApp.MovieApi.Infra.csproj", "MovieApp.MovieApi.Infra/"]
COPY . .
COPY "nuget.config" .
COPY "Packages" .
RUN dotnet restore "MovieApp.MovieApi.API/MovieApp.MovieApi.API.csproj"
COPY . .
WORKDIR "/src/MovieApp.MovieApi.API"
RUN dotnet build "MovieApp.MovieApi.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieApp.MovieApi.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieApp.MovieApi.API.dll"]