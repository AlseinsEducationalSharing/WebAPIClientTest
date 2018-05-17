using System;
using System.Net;
using Alsein.Utilities;
using WebAPIClientTest.Models;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebAPIClientTest.Client
{
    class Program
    {
        private static JsonSerializer JsonSerializer { get; } = new JsonSerializer
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        static void Main(string[] args)
        {
            async void Do()
            {
                (await LoadFromServerAsync(new User
                {
                    UserId = 123,
                    UserName = "Donald Trump",
                    UserPassword = "I am mad"
                })).ForAll(Console.WriteLine);
            }
            Do();
            Console.ReadKey();
        }

        private static async Task<IEnumerable<string>> LoadFromServerAsync(User user, string path = "http://localhost:5000/api/data")
        {
            using (var client = new HttpClient())
            using (var content = new StringContent(JsonConvert.SerializeObject(user)))
            {
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                using (var response = await client.PostAsync(path, content))
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonSerializer.Deserialize<IEnumerable<string>>(jsonReader);
                    }
                    else
                    {
                        return new[] { "Cannot connect to the server." };
                    }
                }
            }
        }
    }
}
