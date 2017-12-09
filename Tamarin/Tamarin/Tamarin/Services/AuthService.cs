using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tamarin.Models;

namespace Tamarin.Services
{
    public static class AuthService
    {
        static HttpClient client;

        static AuthService()
        {
            client = new HttpClient();
            client.BaseAddress = ConstantService.GetUrl();
            client.MaxResponseContentBufferSize = 256000;
        }

        public static async Task<HttpResponseMessage> Login(LoginModel model)
        {
            var route = "account/login";
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(route, content);

            return response;
        }
    }
}
