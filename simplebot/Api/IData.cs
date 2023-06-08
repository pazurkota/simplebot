namespace simplebot.Api; 

public interface IDataFetcher {
    string FetchData();
}

public interface IDataParser {
    List<T> ParseData<T>(string content);
}

public interface IContentFetcher {
    string FetchData(string endpoint);
}