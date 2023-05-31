using RestSharp;
using Newtonsoft.Json;
using simplebot.Classes;

namespace simplebot.Api; 

public class ExcuseApiFetcher : IDataFetcher {
    public string FetchData() {
        RestClientOptions options = new RestClientOptions("https://excuser-three.vercel.app/v1/") {
            ThrowOnAnyError = true
        };

        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("excuse");

        string response = client.Get(request).Content;

        return response ?? throw new Exception("Response is null or invalid");
    }
}

public class ExcuseApiParser : IDataParser {
    public List<ExcuseClass> ParseData<ExcuseClass>(string content) {
        var json = JsonConvert.DeserializeObject<List<ExcuseClass>>(content);

        if (json == null) {
            throw new Exception("JSON is null");
        }

        return json;
    }
}

public class ExcuseApiProcessor {
    private readonly IDataFetcher _fetcher;
    private readonly IDataParser _parser;

    public ExcuseApiProcessor(IDataFetcher fetcher, IDataParser parser) {
        _fetcher = fetcher;
        _parser = parser;
    }
    
    public List<ExcuseClass> ProcessData() {
        var data = _fetcher.FetchData();
        var parsedData = _parser.ParseData<ExcuseClass>(data);

        return parsedData;
    }
}