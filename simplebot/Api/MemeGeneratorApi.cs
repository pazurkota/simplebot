using Newtonsoft.Json;
using RestSharp;

namespace simplebot.Api {
    public class MemeGeneratorApi : IContentFetcher {
        public string FetchData(string endpoint) {
            try {
                string baseUrl = "https://api.memegen.link/";
                string content = $"images/{endpoint}";

                string response = baseUrl + content;
                return response;

            } catch(Exception exc) {
                Console.WriteLine($"Error: {exc}");
                throw;
            }
        }
    }
}