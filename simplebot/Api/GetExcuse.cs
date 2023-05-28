﻿using RestSharp;
using Newtonsoft.Json;
using simplebot.Classes;

namespace simplebot.Api; 

public class GetExcuse : IApiRequest {
    private const string BaseUrl = "https://excuser-three.vercel.app/v1/";
    
     public string GetRequest() {
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

    public List<ExcuseClass> ParseData() {
        try {
            var data = GetRequest();
            var excuse = JsonConvert.DeserializeObject<List<ExcuseClass>>(data);

            if (excuse == null) {
                throw new Exception("Excuse is null");
            }
            
            return excuse;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}