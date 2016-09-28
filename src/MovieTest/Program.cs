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
    public class RootObject
    {
        public string Title { get; set; }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var client = new RestClient("http://www.omdbapi.com/");
            var request = new RestRequest("?t=argo&y=&plot=short&r.json", Method.GET);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            {
                JObject movieList = JsonConvert.DeserializeObject<JObject>(response.Content);
                foreach (var rootObject in movieList)
                {

                    Console.WriteLine(rootObject);
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
    //    var movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonResponse["Title"].ToString());
}

