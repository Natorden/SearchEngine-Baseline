﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;

namespace ConsoleSearch
{
    public class App
    {
        public void Run()
        {
            HttpClient api = new HttpClient();
            api.BaseAddress = new Uri("http://localhost:8889");
            
            Console.WriteLine("Console Search");
            
            while (true)
            {
                Console.WriteLine("enter search terms - q for quit");
                string input = Console.ReadLine() ?? string.Empty;
                if (input.Equals("q")) break;

                Task<string> task = api.GetStringAsync("/Search?terms=" + input + "&numberOfResults=10");
                task.Wait();
                string resultString = task.Result;
                SearchResult result = JsonConvert.DeserializeObject<SearchResult>(resultString);

                foreach (var ignored in result.IgnoredTerms)
                {
                    Console.WriteLine(ignored + "was ignored");
                }

                foreach (var resultDocument in result.Documents)
                {
                    Console.WriteLine(resultDocument.Id + ":" + resultDocument.Path + "Number of terms found: " + resultDocument.NumberOfOccurences);
                }
                Console.WriteLine("Found "+ result.Documents.Count + "in: " + result.ElapsedMlliseconds + "ms");

            }
        }
    }
}
