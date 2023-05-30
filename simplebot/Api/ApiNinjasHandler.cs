using Newtonsoft.Json;
using RestSharp;

namespace simplebot.Api; 

public class ApiNinjasHandler {
    private const string BaseUrl = "https://api.api-ninjas.com/v1/";

    private string GetRequest(string endpoint) {
        var apiKey = ConfigHandler.GetConfig().ApiNinjasApiKey;
        
        var client = new RestClient(BaseUrl);
        var request = new RestRequest($"{endpoint}?limit=1").AddHeader("X-Api-Key", apiKey);

        var response = client.Execute(request).Content;

        if (response == null) {
            throw new Exception("Response is null");
        }

        return response;
    }

    public List<T> ParseData<T>(string endpoint) {
        try {
            if (endpoint == null) {
                throw new Exception("Endpoint is null");
            }
            
            var data = GetRequest(endpoint);
            var json = JsonConvert.DeserializeObject<List<T>>(data);

            if (json == null) {
                throw new Exception("Response is null");
            }
            
            return json;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}