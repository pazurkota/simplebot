using Newtonsoft.Json;
using RestSharp;
using simplebot.Classes;

namespace simplebot.Api; 

public class GetRandomFact : IApiRequest {
    private const string BaseUrl = "https://api.api-ninjas.com/v1/";
    
    public string GetRequest() {
        try {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("facts?limit=1");
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

    public FactClass ParseData() {
        try {
            var data = GetRequest();
            var fact = JsonConvert.DeserializeObject<FactClass>(data);

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