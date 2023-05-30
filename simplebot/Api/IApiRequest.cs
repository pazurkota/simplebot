namespace simplebot.Api; 

public interface IApiRequest {
    List<T> ParseData<T>(string endpoint);
}