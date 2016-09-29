using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MovieTest
{
     public class MovieClass
    {
        public string Title { get; set; }
        public string Year { get; set; }
    }
    
    class Program
    {
        
        static void Main(string[] args)
        {
            var input = "braveheart";
            var client = new RestClient("http://www.omdbapi.com/");
            var request = new RestRequest("?t=" + input + "&y=&plot=short&r.json", Method.GET);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            {

             MovieClass movieList = JsonConvert.DeserializeObject<MovieClass>(response.Content);
             Dictionary<string, string> movieList2 = new Dictionary<string, string>()
             {
                 {"Title", movieList.Title },
                 {"Year", movieList.Year }
             };

             foreach (var pair in movieList2)
                {
                    Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
                }
              Console.ReadLine();
            }
        }
        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}

