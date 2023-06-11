using Newtonsoft.Json;
using RestSharp;
using simplebot.Configuration;

namespace simplebot.Engine.Api; 

public class FactApiFetcher : simplebot.Engine.Api.IDataFetcher {
    public string FetchData() {
        string apiKey = Config.LoadConfig().ApiNinjasApiKey;
        
        RestClientOptions options = new RestClientOptions("https://api.api-ninjas.com/v1/") {
            ThrowOnAnyError = true
        };

        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("facts?limit=1").AddHeader("X-Api-Key", apiKey);

        string response = client.Get(request).Content ?? throw new Exception("Response is null or invalid");
        
        return response;
    }
}

public class FactApiParser : simplebot.Engine.Api.IDataParser {
    public List<FactClass> ParseData<FactClass>(string content) {
        try {
            var data = content;
            var json = JsonConvert.DeserializeObject<List<FactClass>>(data);

            if (json == null) {
                throw new Exception("JSON is null");
            }
            
            return json;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}

public class FactApiProcessor {
    private readonly simplebot.Engine.Api.IDataFetcher _fetcher;
    private readonly simplebot.Engine.Api.IDataParser _parser;
    
    public FactApiProcessor(simplebot.Engine.Api.IDataFetcher fetcher, simplebot.Engine.Api.IDataParser parser) {
        _fetcher = fetcher;
        _parser = parser;
    }
    
    public List<simplebot.Engine.Classes.FactClass> ProcessData() {
        var data = _fetcher.FetchData();
        var parsedData = _parser.ParseData<simplebot.Engine.Classes.FactClass>(data);

        return parsedData;
    }
}