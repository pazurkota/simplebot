namespace simplebot.Api; 

public interface IDataFetcher {
    string FetchData();
}

public partial interface IDataParser {
    List<T> ParseData<T>(string content);
}

public partial interface ISingleDataParser {
    T ParseData<T>(string content);
}

public interface IContentFetcher {
    string FetchData(string endpoint);
}