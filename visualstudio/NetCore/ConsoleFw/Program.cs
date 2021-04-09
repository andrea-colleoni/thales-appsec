using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFw
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            var client = new APIClientStandard.Client("https://localhost:5001", httpClient);
            var persone = client.ApiPersoneGetAsync().Result;
            foreach(var p in persone)
            {
                Console.WriteLine($"Nome: {p.Nome}, Cognome: {p.Cognome}");
            }
        }
    }
}
