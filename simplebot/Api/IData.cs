namespace simplebot.Api; 

public interface IDataFetcher {
    string FetchData();
}

public interface IDataParser {
    List<T> ParseData<T>();
}