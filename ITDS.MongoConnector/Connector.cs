using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITDS.MongoConnector
{
    public class Connector
    {
        private MongoClient _client;
        private IMongoDatabase _database;

        public Connector()
        {
            _client = new MongoClient("mongodb+srv://ITDS_USER:ITDS_PASS@testcluster-ttkfd.mongodb.net/sample_mflix?retryWrites=true&w=majority");
            _database = _client.GetDatabase("sample_mflix");
        }

        public async Task<string> FilterMovies(int yearLowerBand, int yearUpperBand) 
        {
            var movies = new List<BsonDocument>();

            var projectionBuilder = new ProjectionDefinitionBuilder<BsonDocument>();
            var projection = projectionBuilder
                .Include("awards")
                .Include("genres")
                .Include("plot")
                .Include("rated")
                .Include("title")
                .Include("year")
                .Exclude("_id");

            var filterBuilder = new FilterDefinitionBuilder<BsonDocument>();
            var filter = filterBuilder.Gte("year", yearLowerBand) & filterBuilder.Lte("year", yearUpperBand);
            using (IAsyncCursor<BsonDocument> documents = await _database.GetCollection<BsonDocument>("movies").FindAsync(filter))
            {
                while (documents.MoveNext())
                {
                    movies.AddRange(documents.Current);
                }
            }

            return movies.ToJson();
        }

        public async Task<List<Movie>> GetMovies(int yearLowerBand, int yearUpperBand) 
        {
            var movies = new List<Movie>();

            var projectionBuilder = new ProjectionDefinitionBuilder<Movie>();
            var projection = projectionBuilder
                .Include(x => x.Awards)
                .Include(x => x.Genres)
                .Include(x => x.Plot)
                .Include(x => x.Rated)
                .Include(x => x.Title)
                .Include(x => x.Year)
                .Exclude("_id");

            var filterBuilder = new FilterDefinitionBuilder<Movie>();
            var filter = filterBuilder.Gte(x => x.Year, yearLowerBand) & filterBuilder.Lte(x => x.Year, yearUpperBand);

            using (IAsyncCursor<Movie> documents = await _database.GetCollection<Movie>("movies")
                .FindAsync(filter, new FindOptions<Movie, Movie>
                {
                    Projection = projection
                }))
            {
                while (documents.MoveNext())
                {
                    movies.AddRange(documents.Current);
                }
            }

            return movies;
        }
    }
}
