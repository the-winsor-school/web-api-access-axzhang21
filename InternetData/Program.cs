﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Net.Http;

namespace InternetData
{
    class MainClass
    {
        public static void GetRonSwansonQuotes()
        {
            HttpClient client = new HttpClient();

            Console.Write("How many quotes do you want?  ");
            int count = Convert.ToInt32(Console.ReadLine());//declaration of variable count 

            // Create an HTTP GET request for the API Endpoint.
            HttpRequestMessage request =
                new HttpRequestMessage(HttpMethod.Get,
                // This is the URL for the API endpoint, the last {0} is where you insert the number /count/ into the string.
                string.Format("https://ron-swanson-quotes.herokuapp.com/v2/quotes/{0}", count));//the count (number of quotes user wants) is entered into the {0} as an integer

            // Use the HttpClient to send the request message to the remote server.
            // The result, is a Response message which contains the data you requested.
            HttpResponseMessage response = client.SendAsync(request).Result;//send request
                                                                            //if in webbrowser, equivalent of hitting go or enter; when browser is loading;the response is stored in memory, including what information was requested
                                                                            //every response has a status code; just a quick check to see if content is actually sent back

            // Since the data is in JSON format, we use a DataContractJsonSerializer to pull the data out of that object.
            // the JsonSerializer takes a DataType as its input.  in this case, we expect the response to be a List of Strings.
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<string>));//don't need to know it to use it
                                                                                                         //takes JSON data and spits out
                                                                                                         //new DataContractJsonSerializer-constructor method, initializing
                                                                                                         //typeof(List<string>))-pass it the type of data getting from http response
                                                                                                         //overloaded methods-one moethod with different parameters 
         
            // Make sure the response is actually there and not a failed request.
            if (response.IsSuccessStatusCode)
            {
                // Read the data from the response message.  The result is an Object that is converted into a List<string> type.
                List<string> quotes = (List<string>)serializer.ReadObject(response.Content.ReadAsStreamAsync().Result); //

                // Print each of the quotes!
                foreach (string quote in quotes)
                {
                    Console.WriteLine(quote);
                }
            }

            // If something went wrong...
            else
            {
                // Make the text red
                Console.ForegroundColor = ConsoleColor.Red;

                // print the status code for the failure.  see https://http.cat for interpretations
                Console.WriteLine("Failed!  Status Code: {0}", response.StatusCode);
            }
        }


        public static void Main(string[] args)
        {
            Example example = Example.LoadExample();
            Console.WriteLine(example);

            //WeatherForcast forcast = Weather.GetWeatherForcast();

            //GetRonSwansonQuotes();

            //Console.WriteLine(forecast.currently.summary);

            Console.WriteLine("\nDone!");
        }
    }
}
