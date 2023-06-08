using Newtonsoft.Json;
using RestSharp;
using simplebot.Classes;

namespace simplebot.Api; 

public class InsultApiFetcher : IDataFetcher {
    public string FetchData() {
        try {
            RestClientOptions options = new RestClientOptions("https://evilinsult.com/") {
                ThrowOnAnyError = true
            };
            
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest("generate_insult.php?lang=en&type=json");
            
            string response = client.Get(request).Content ?? throw new Exception("Response is null or invalid");
            return response;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}

public class InsultApiParser : ISingleDataParser {
    public InsultClass ParseData<InsultClass>(string content) {
        try {
            var data = content;
            var json = JsonConvert.DeserializeObject<InsultClass>(data);

            if (json == null) {
                throw new Exception("Insult is null");
            }
            
            return json;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}

public class InsultApiProcessor {
    private readonly IDataFetcher _fetcher;
    private readonly ISingleDataParser _parser;
    
    public InsultApiProcessor(IDataFetcher fetcher, ISingleDataParser parser) {
        _fetcher = fetcher;
        _parser = parser;
    }
    
    public InsultClass ProcessData() {
        var data = _fetcher.FetchData();
        var parsedData = _parser.ParseData<InsultClass>(data);

        return parsedData;
    }
}