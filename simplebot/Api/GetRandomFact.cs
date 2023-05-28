using Newtonsoft.Json;
using RestSharp;
using simplebot.Classes;

namespace simplebot.Api; 

public class GetRandomFact : ApiRequest {
    private const string BaseUrl = "https://api.api-ninjas.com/v1/";

    protected override string GetRequest() {
        try {
            var apiKey = ConfigHandler.GetConfig().ApiNinjasApiKey;
            
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("facts?limit=1").AddHeader("X-Api-Key", apiKey);
            
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

    public override List<FactClass> ParseData<FactClass>() {
        try {
            var data = GetRequest();
            var fact = JsonConvert.DeserializeObject<List<FactClass>>(data);

            if (fact == null) {
                throw new Exception("Fact is null");
            }
            
            return fact;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}