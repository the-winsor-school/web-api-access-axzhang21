using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Net.Http;

namespace StockData
{
    public class StockData
    {
        public static void Main(string[] args)
        {
            //main code
            StockRealTimePrice friend = GetStockRealTimePrice();
            Console.WriteLine("Stock:APPL");
            Console.WriteLine(friend.price);
            Console.ReadLine();
        }
        public static StockRealTimePrice GetStockRealTimePrice()//class to get the real time stock price 
        {
            //gets under web browser-communicate http with server
            HttpClient client = new HttpClient();

            //creating http request with a method (get.)
            //get request is when you're looking at a website
            //client sends request
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,string.Format("https://financialmodelingprep.com/api/v3/stock/real-time-price/AAPL"));

            //returns response
            //serializer is taking the JSON and converting it to C#
            HttpResponseMessage response = client.SendAsync(request).Result;

            //typeof-StockRealTimePrice- is a type
            //expecting actual DataType itself (type is also an object)
            //using typeof gives the datatype 
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(StockRealTimePrice));

            if (!response.IsSuccessStatusCode)
            {
                //StockRealTimePrice friend = (StockRealTimePrice)serializer.ReadObject(response.Content.ReadAsStreamAsync().Result);
                
                return new StockRealTimePrice();

            }

            return (StockRealTimePrice)serializer.ReadObject(response.Content.ReadAsStreamAsync().Result);
        }

        // return (StockRealTimePrice)serializer.ReadObject(response.Content.ReadAsStreamAsync().Result);
    }


    [DataContract]
    public class StockRealTimePrice //I wrote this class to return Real Times Stock Prices
    {
        [DataMember]
        public string symbol;

        [DataMember]
        public double price;
    }

}





//while true then call function 