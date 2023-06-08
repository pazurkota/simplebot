using Newtonsoft.Json;
using RestSharp;

namespace simplebot.Api {
    public class MemeGeneratorApi : IContentFetcher {
        public string FetchData(string endpoint) {
            try{
                RestClientOptions options = new RestClientOptions("https://api.memegen.link/") {
                    ThrowOnAnyError = true
                };

                RestClient client = new RestClient(options);
                RestRequest request = new RestRequest($"/images/{endpoint}");
                
                string response = client.Get(request).Content ?? throw new Exception("Response is null or invalid");

                return response;

            } catch(Exception exc) {
                Console.WriteLine($"Error: {exc}");
                throw;
            }
        }
    }
}