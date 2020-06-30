using Grpc.Net.Client;
using ITDS.Grpc.Service;
using System;
using System.Threading.Tasks;

namespace ITDS.Grpc.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string method = "";
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Service.ITDS.ITDSClient(channel);

            while (method != "X") {
                Console.WriteLine("Choose method [1/2/X]:");
                method = Console.ReadLine();

                if (method == "1")
                {
                    var response = await client.FilterMovieByYearAsync(new MovieByYearRequest
                    {
                        YearLowerBand = 1982,
                        YearUpperBand = 1983
                    });

                    Console.WriteLine(response.Data);
                }
                else if (method == "2") 
                {
                    var response = await client.FilterMovieByYearStronglyTypedAsync(new MovieByYearRequest
                    {
                        YearLowerBand = 1982,
                        YearUpperBand = 1983
                    });

                    foreach(var movie in response.Movie) 
                    {
                        Console.WriteLine(movie.Title);
                    }
                }
            }
        }
    }
}
