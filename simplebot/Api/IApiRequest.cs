namespace simplebot.Api; 

public interface IApiRequest {
    string GetRequest();
    T ParseData<T>(string data);
}