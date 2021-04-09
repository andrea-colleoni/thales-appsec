using System;
using System.Net.Http;

namespace ConsoleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            HttpClient httpClient = new HttpClient();
            var client = new APIClientStandard.Client("https://localhost:5001", httpClient);
            var persone = client.ApiPersoneGetAsync().Result;
            foreach (var p in persone)
            {
                Console.WriteLine($"Nome: {p.Nome}, Cognome: {p.Cognome}");
            }

            Console.ReadLine();
        }
    }
}
