using Newtonsoft.Json;
using RestSharp;

namespace simplebot.Engine.Api; 

public class ExcuseApiFetcher : simplebot.Engine.Api.IDataFetcher {
    public string FetchData() {
        RestClientOptions options = new RestClientOptions("https://excuser-three.vercel.app/v1/") {
            ThrowOnAnyError = true
        };

        RestClient client = new RestClient(options);
        RestRequest request = new RestRequest("excuse");

        string response = client.Get(request).Content ?? throw new Exception("Response is null or invalid");

        return response;
    }
}

public class ExcuseApiParser : simplebot.Engine.Api.IDataParser {
    public List<ExcuseClass> ParseData<ExcuseClass>(string content) {
        var json = JsonConvert.DeserializeObject<List<ExcuseClass>>(content);

        if (json == null) {
            throw new Exception("JSON is null");
        }

        return json;
    }
}

public class ExcuseApiProcessor {
    private readonly simplebot.Engine.Api.IDataFetcher _fetcher;
    private readonly simplebot.Engine.Api.IDataParser _parser;

    public ExcuseApiProcessor(simplebot.Engine.Api.IDataFetcher fetcher, simplebot.Engine.Api.IDataParser parser) {
        _fetcher = fetcher;
        _parser = parser;
    }
    
    public List<simplebot.Engine.Classes.ExcuseClass> ProcessData() {
        var data = _fetcher.FetchData();
        var parsedData = _parser.ParseData<simplebot.Engine.Classes.ExcuseClass>(data);

        return parsedData;
    }
}