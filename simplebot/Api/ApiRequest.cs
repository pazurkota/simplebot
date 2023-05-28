namespace simplebot.Api; 

public abstract class ApiRequest {
    protected abstract string GetRequest();
    public abstract List<T> ParseData<T>();
}