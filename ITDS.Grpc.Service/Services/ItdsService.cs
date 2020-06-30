using Grpc.Core;
using ITDS.MongoConnector;
using System.Threading.Tasks;

namespace ITDS.Grpc.Service
{
    public class ItdsService : ITDS.ITDSBase
    {
        private Connector _connector;

        public ItdsService(Connector connector)
        {
            _connector = connector;
        }

        public override async Task<MovieByYearResponse> FilterMovieByYear(MovieByYearRequest request, ServerCallContext context)
        {
            var movies = await _connector.FilterMovies(request.YearLowerBand, request.YearUpperBand);

            return new MovieByYearResponse 
            { 
                Data = movies
            };
        }

        public override async Task<MovieByYearStronglyTypedResponse> FilterMovieByYearStronglyTyped(MovieByYearRequest request, ServerCallContext context)
        {
            var movies = await _connector.GetMovies(request.YearLowerBand, request.YearUpperBand);

            var response = new MovieByYearStronglyTypedResponse();

            foreach (var movie in movies) 
            {
                var movieToBeAdded = new Movie
                {
                    Awards = new Movie.Types.Award
                    {
                        Nominations = movie.Awards.Nominations,
                        Wins = movie.Awards.Wins,
                        Text = movie.Awards.Text ?? ""
                    },
                    Plot = movie.Plot ?? "",
                    Rated = movie.Rated ?? "",
                    Title = movie.Title ?? "",
                    Year = movie.Year
                };
                if (movie.Genres != null)
                {
                    movieToBeAdded.Genres.AddRange(movie.Genres);
                }

                response.Movie.Add(movieToBeAdded);
            }

            return response;
        }
    }
}
