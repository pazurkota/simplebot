using Newtonsoft.Json;
using RestSharp;

namespace simplebot.Api {
    public class MemeGeneratorApi : IContentFetcher {
        public string FetchData(string endpoint) {
            try {
                string baseUrl = "https://api.memegen.link/";
                
                string content = $"images/{endpoint}";
                content = CheckForSpaces(content);
                
                string response = baseUrl + content;
                return response;

            } catch(Exception exc) {
                Console.WriteLine($"Error: {exc}");
                throw;
            }
        }
        
        private string CheckForSpaces(string content) {
            if (content.Contains(" ")) {
                content = content.Replace(" ", "%20");
            }

            return content;
        }
    }
}