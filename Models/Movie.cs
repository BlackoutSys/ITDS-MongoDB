using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Models
{
    public class Movie
    {
        [BsonElement("plot")]
        public string Plot { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("year")]
        public int Year { get; set; }
        [BsonElement("rated")]
        public string Rated { get; set; }
        [BsonElement("awards")]
        public Award Awards { get; set; }
        [BsonElement("genres")]
        public IEnumerable<string> Genres { get; set; }
    }
}
