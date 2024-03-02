using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;

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

        public async Task<T> PostApiResponse<T>(string endpoint, T data)
        {
            try
            {
                // Convert the data object to JSON
                string jsonData = JsonSerializer.Serialize(data);

                // Create HttpContent with JSON data
                HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Send POST request
                HttpResponseMessage response = await _httpClient.PostAsync(endpoint, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    // Read content from the response
                    var content = await response.Content.ReadAsStringAsync();

                    // Handle Ok() values without JSON format
                    if (response.Content.Headers.ContentType?.MediaType == "application/json")
                    {
                        // If the content type is JSON, deserialize it
                        T result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true // Optional: Disable case sensitivity.
                        });

                        return result;
                    }
                    else
                    {
                        // Handle non-JSON content, if necessary
                        Console.WriteLine($"Non-JSON Content: {content}");
                        return default(T);
                    }
                }
                else
                {
                    // Handle error cases
                    var statusCode = response.StatusCode;
                    var errorContent = await response.Content.ReadAsStringAsync();

                    // Log or handle the error information
                    Console.WriteLine($"Error {statusCode}: {errorContent}");

                    // Optionally, throw an exception or handle the error based on your requirements
                    throw new HttpRequestException($"Error {statusCode}: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                // Handle other exceptions if any
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Optionally, throw an exception or handle the error based on your requirements
                throw;
            }
        }




    }
}
