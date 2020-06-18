using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient("mongodb+srv://ITDS_USER:ITDSPASS@testcluster-ttkfd.mongodb.net/sample_mflix?retryWrites=true&w=majority");
            var database = client.GetDatabase("sample_mflix");

            var filterBuilder = new FilterDefinitionBuilder<BsonDocument>();
            var filter = filterBuilder.Lte("year", 1983);
            using (IAsyncCursor<BsonDocument> documents = database.GetCollection<BsonDocument>("movies").FindSync(filter))
            {
                while (documents.MoveNext())
                {
                    foreach(var doc in documents.Current)
                    {
                        Console.WriteLine(doc.ToJson());
                    }
                }
            }
        } 
    }
}
