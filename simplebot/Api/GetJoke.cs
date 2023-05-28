using Newtonsoft.Json;
using RestSharp;

namespace simplebot.Api; 

public class GetJoke : ApiRequest {
    private const string BaseUrl = "https://api.api-ninjas.com/v1/";
    
    protected override string GetRequest() {
        try {
            var apiKey = ConfigHandler.GetConfig().ApiNinjasApiKey;
            
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("jokes?limit=1").AddHeader("X-Api-Key", apiKey);
            
            var response = client.Get(request).Content;
            
            if (response == null) {
                throw new Exception("Response is null");
            }
            
            return response;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public override List<T> ParseData<T>() {
        try {
            var data = GetRequest();
            var joke = JsonConvert.DeserializeObject<List<T>>(data);

            if (joke == null) {
                throw new Exception("Joke is null");
            }
            
            return joke;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}