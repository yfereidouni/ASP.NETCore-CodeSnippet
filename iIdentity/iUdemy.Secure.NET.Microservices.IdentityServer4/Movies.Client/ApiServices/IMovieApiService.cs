using Movies.Client.Models;

namespace Movies.Client.ApiServices;

public interface IMovieApiService
{
    Task<IEnumerable<Movie>> GetMovies();
    Task<Movie> GetMovie(int id);
    Task<Movie> CreateMovie(Movie movie);
    Task<Movie> UpdateMovie(Movie movie);
    Task DeleteMovie(int id);
}