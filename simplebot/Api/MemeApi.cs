using Newtonsoft.Json;
using RestSharp;
using simplebot.Classes;

namespace simplebot.Api;

public class MemeApiFetcher : IDataFetcher {
    public string FetchData() {
        try
        {
            RestClientOptions options = new RestClientOptions("https://meme-api.com/") {
                ThrowOnAnyError = true
            };

            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest("gimme").AddHeader("Accept", "application/json");

            string response = client.Get(request).Content ?? throw new Exception("Response is null or invalid");
            return response;   
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

public class MemeApiParser : ISingleDataParser {
    public MemeClass ParseData<MemeClass>(string content) {
        try
        {
            var json = JsonConvert.DeserializeObject<MemeClass>(content);

            if (json is null) {
                throw new Exception("Json is null or invalid");
            }

            return json;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

public class MemeApiProcessor {
    private readonly IDataFetcher _fetcher;
    private readonly ISingleDataParser _parser;

    public MemeApiProcessor(IDataFetcher fetcher, ISingleDataParser parser) {
        _fetcher = fetcher;
        _parser = parser;
    }

    public MemeClass ProcessData(){
        var data = _fetcher.FetchData();
        var parsedData = _parser.ParseData<MemeClass>(data);

        return parsedData;
    }
}