using IdentityModel.Client;
using Movies.Client.Models;
using Newtonsoft.Json;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            // Steps:
            // 1 - Get Token from Identity Server, of course we should provide the IS Configuration like address, clientId and client secret.
            // 2 - Send Request to Protected API 
            // 3 - Deserializing Object to MovieList

            // 1. "retrieve" our api credentials. This must be registered on Isentity Server!
            var apiClientCredentials = new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:5005/connect/token",
                
                ClientId = "movieClient",
                ClientSecret = "secret",
                
                // This is the scope our Protected API requires
                Scope="movieAPI"
            };
            
            // create a new HttpClient to talk to our IdentityServer (localhost:5005)
            var client = new HttpClient();

            // just check if we can reach the Dicovery document. Not 100% needed but...
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5005");
            if (disco.IsError)
            {
                return null; // throw 500 error
            }

            // 2. Authenticates and get an access token from Identity Server
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiClientCredentials);
            if (tokenResponse.IsError)
            {
                return null;
            }

            // 2 - Send Request to Protected API

            // Another HttpClient for talking now with our Protected API
            var apiClient = new HttpClient();

            // 3. Set the access_token in the request Authorization: Bearer <token>
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            // 4. Send a request to our Protected API
            var response = await apiClient.GetAsync("https://localhost:5001/api/movies/get");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            List<Movie> movieList = JsonConvert.DeserializeObject<List<Movie>>(content);

            return movieList;

            //var movieList = new List<Movie>();
            //movieList.Add(
            //    new Movie
            //    {
            //        Id = 1,
            //        Genre = "Comics",
            //        Title = "asd",
            //        Rating = "9.2",
            //        ImageUrl = "images/src",
            //        ReleaseDate = DateTime.Now,
            //        Owner = "swn"
            //    });

            //return await Task.FromResult(movieList);
        }

        public Task<Movie> GetMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
