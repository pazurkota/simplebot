using Newtonsoft.Json;
using RestSharp;
using simplebot.Classes;

namespace simplebot.Api; 

public class JokeApiFetcher : IDataFetcher {
    public string FetchData() {
        string apiKey = ConfigHandler.GetConfig().ApiNinjasApiKey;
        
        RestClientOptions options = new RestClientOptions("https://api.api-ninjas.com/v1/") {
            ThrowOnAnyError = true
        };

        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("jokes?limit=1").AddHeader("X-Api-Key", apiKey);
        
        string response = client.Get(request).Content ?? throw new Exception("Response is null or invalid");

        return response;
    }
}

public class JokeApiParser : IDataParser {
    public List<JokeClass> ParseData<JokeClass>(string content) {
        try {
            var data = content;
            var json = JsonConvert.DeserializeObject<List<JokeClass>>(data);

            if (json == null) {
                throw new Exception("Joke is null");
            }
            
            return json;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}

public class JokeApiProcessor {
    private readonly IDataFetcher _fetcher;
    private readonly IDataParser _parser;
    
    public JokeApiProcessor(IDataFetcher fetcher, IDataParser parser) {
        _fetcher = fetcher;
        _parser = parser;
    }
    
    public List<JokeClass> ProcessData() {
        var data = _fetcher.FetchData();
        var parsedData = _parser.ParseData<JokeClass>(data);

        return parsedData;
    }
}