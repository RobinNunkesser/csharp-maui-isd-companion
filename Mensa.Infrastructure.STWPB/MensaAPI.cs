using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mensa.Infrastructure.STWPB
{
    public class MensaAPI
    {
        public static HttpClient HttpClient;        

        public MensaAPI()
        {
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://www.studentenwerk-pb.de")
            };

            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Meal>> GetMeals()
        {
            var meals = await HttpClient.GetFromJsonAsync<List<Meal>>($"fileadmin/shareddata/access2.php?id={Secret.id}");
            return meals;
        }
    }
}
