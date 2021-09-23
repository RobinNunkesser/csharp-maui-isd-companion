using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mensa.Infrastructure.STWPB
{
    public class MensaAPI
    {
        public static HttpClient HttpClient;

        public MensaAPI(string acceptLanguage)
        {
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://www.studentenwerk-pb.de")
            };

            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(acceptLanguage));
        }

        public async Task<List<Meal>> GetMeals()
        {
            var meals = await HttpClient.GetFromJsonAsync<List<Meal>>($"fileadmin/shareddata/access2.php?id={Secret.id}", Converter.Options);
            return meals;
        }

        public async Task<List<Meal>> GetTodaysHammMeals()
        {
            var meals = await HttpClient.GetFromJsonAsync<List<Meal>>($"fileadmin/shareddata/access2.php?id={Secret.id}&restaurant=mensa-hamm&date=2021-09-27", Converter.Options);
            return meals;
        }
    }
}
