using Newtonsoft.Json;
using PlaUno.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PlaUno.Helpers
{
    public class ApiService
    {
        public static async Task<Response> LoginAsync(LoginRequest model)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");

                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://localhost:44314/")
                };

                HttpResponseMessage response = await client.PostAsync("api/Account/Login", content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                User user = JsonConvert.DeserializeObject<User>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = user,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
