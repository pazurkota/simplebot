﻿using RestSharp;
using Newtonsoft.Json;
using simplebot.Classes;

namespace simplebot.Api; 

public class GetExcuse : IApiRequest {
    private const string BaseUrl = "https://excuser-three.vercel.app/v1/";

    private string GetRequest() {
        try {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("excuse");
            var response = client.Get(request).Content;
            
            if (response == null) {
                throw new Exception("Response is null");
            }
            
            return response;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<T> ParseData<T>(string endpoint) {
        try {
            var data = GetRequest();
            var json = JsonConvert.DeserializeObject<List<T>>(data);

            if (json == null) {
                throw new Exception("Excuse is null");
            }
            
            return json;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}