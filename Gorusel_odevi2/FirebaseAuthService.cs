using System;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace Gorusel_odevi2
{
    public class FirebaseService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string FirebaseApiKey = "AIzaSyDcSoDh8MBx-guoNtMNUEeFwRjyy75tPrg"; 
        private const string DatabaseUrl = "https://mohammedodevdata3-default-rtdb.firebaseio.com/"; 


     
        public async Task<string> RegisterUserAsync(string email, string password)
        {
            var request = new
            {
                email,
                password,
                returnSecureToken = true
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={FirebaseApiKey}", content);

            if (response.IsSuccessStatusCode)
            {
                return "Kayıt başarılı!";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Kayıt başarısız: {error}";
            }
        }


        public async Task<string> LoginUserAsync(string email, string password)
        {
            var request = new
            {
                email,
                password,
                returnSecureToken = true
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={FirebaseApiKey}", content);

            if (response.IsSuccessStatusCode)
            {
                return "Giriş başarılı!";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Giriş başarısız: {error}";
            }
        }

       
        public async Task<string> WriteDataAsync(string path, object data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{DatabaseUrl}{path}.json", content);

            if (response.IsSuccessStatusCode)
            {
                return "Data written successfully!";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Error writing data: {error}";
            }
        }

   
        public async Task<string> ReadDataAsync(string path)
        {
            var response = await _httpClient.GetAsync($"{DatabaseUrl}{path}.json");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Error reading data: {error}";
            }
        }
    }
}
