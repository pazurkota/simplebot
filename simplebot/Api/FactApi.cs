using Newtonsoft.Json;
using RestSharp;
using simplebot.Classes;

namespace simplebot.Api; 

public class FactApiFetcher : IDataFetcher {
    public string FetchData() {
        string apiKey = ConfigHandler.GetConfig().ApiNinjasApiKey;
        
        RestClientOptions options = new RestClientOptions("https://api.api-ninjas.com/v1/") {
            ThrowOnAnyError = true
        };

        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("facts?limit=1").AddHeader("X-Api-Key", apiKey);

        string content = client.Get(request).Content ?? throw new Exception("Response is null");
        
        return content;
    }
}

public class FactApiParser : IDataParser {
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
    private readonly IDataFetcher _fetcher;
    private readonly IDataParser _parser;
    
    public FactApiProcessor(IDataFetcher fetcher, IDataParser parser) {
        _fetcher = fetcher;
        _parser = parser;
    }
    
    public List<FactClass> ProcessData() {
        var data = _fetcher.FetchData();
        var parsedData = _parser.ParseData<FactClass>(data);

        return parsedData;
    }
}