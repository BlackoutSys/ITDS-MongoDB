using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Award
    {
        [BsonElement("wins")]
        public int Wins { get; set; }
        [BsonElement("nominations")]
        public int Nominations { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
    }
}
