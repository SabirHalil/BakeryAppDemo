using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BakeryAppUI.Controllers
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task<T> GetApiResponse<T>(string endpoint)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                // API'den gelen JSON içeriği okuma
                var jsonContent = await response.Content.ReadAsStringAsync();

                //JSON içeriğini belirli bir türde nesneye dönüştürme

               T result = JsonSerializer.Deserialize<T>(jsonContent, new JsonSerializerOptions
               {
                   PropertyNameCaseInsensitive = true // Opsiyonel: Büyük/küçük harf duyarlılığını kapatır.
               });

                return result;
            }
            else
            {
                // Hata durumlarını işleme alma
                // ...
                return default(T);
            }
        }
    }
}
