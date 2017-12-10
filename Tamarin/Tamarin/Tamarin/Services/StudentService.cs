using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tamarin.Models;

namespace Tamarin.Services
{
    public class StudentService
    {
        private static HttpClient client;

        public static async Task<HttpResponseMessage> GetAll()
        {
            client = new HttpClient();
            client.BaseAddress = ConstantService.GetUrl();
            client.MaxResponseContentBufferSize = 256000;

            var studentId = App.Current.Properties["id"] as string;

            var route = "student/getall" + "/" + studentId;

            var token = App.Current.Properties["token"] as string;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(route);

            return response;
        }

        public static async Task<HttpResponseMessage> GetById()
        {
            client = new HttpClient();
            client.BaseAddress = ConstantService.GetUrl();
            client.MaxResponseContentBufferSize = 256000;

            var studentId = App.Current.Properties["id"] as string;

            var route = "student/getbyid" + "/" + studentId;

            var token = App.Current.Properties["token"] as string;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(route);
            
            return response;
        }

        public static async Task<HttpResponseMessage> GetAllBySubject(int id)
        {
            client = new HttpClient();
            client.BaseAddress = ConstantService.GetUrl();
            client.MaxResponseContentBufferSize = 256000;

            var route = "student/getAllBySubject" + "/" + id;

            var token = App.Current.Properties["token"] as string;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(route);

            return response;
        }
    }
}
