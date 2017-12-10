using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tamarin.Models;

namespace Tamarin.Services
{
    public class SubjectService
    {
        private static HttpClient client;

        public static async Task<HttpResponseMessage> GetAll(bool isConfirmed)
        {
            client = new HttpClient();
            client.BaseAddress = ConstantService.GetUrl();
            client.MaxResponseContentBufferSize = 256000;

            var studentId = App.Current.Properties["id"] as string;

            var route = "";
            if(isConfirmed)
                route = "subject/getall" + "/" + studentId;
            else
                route = "subject/getallUnconfirmed" + "/" + studentId;

            var token = App.Current.Properties["token"] as string;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(route);

            return response;
        }

        public static async Task<HttpResponseMessage> ConfirmSubject(SubjectModel model)
        {
            client = new HttpClient();
            client.BaseAddress = ConstantService.GetUrl();
            client.MaxResponseContentBufferSize = 256000;

            var route = "subject/update";

            model.Confirmations++;

            var token = App.Current.Properties["token"] as string;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(route, content);

            return response;
        }

        public static async Task<HttpResponseMessage> Add(SubjectModel model)
        {
            client = new HttpClient();
            client.BaseAddress = ConstantService.GetUrl();
            client.MaxResponseContentBufferSize = 256000;

            var route = "subject/add";
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var token = App.Current.Properties["token"] as string;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync(route, content);

            return response;
        }
    }
}
