namespace simplebot.Api; 

public interface IApiRequest {
    string GetRequest();
    List<T> ParseData<T>();
}