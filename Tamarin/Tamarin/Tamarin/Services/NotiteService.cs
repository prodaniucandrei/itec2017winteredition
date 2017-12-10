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
    public class NotiteService
    {
        private static HttpClient client;

        public static async Task<HttpResponseMessage> GetNotite(int subjectId)
        {
            client = new HttpClient();
            client.BaseAddress = ConstantService.GetUrl();
            client.MaxResponseContentBufferSize = 256000;

            var route = "note/getallbysubject" + "/" + subjectId;
            var token = App.Current.Properties["token"] as string;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(route);

            return response;
        }

        public static async Task<HttpResponseMessage> PostNote(NoteModel note)
        {
            var route = "note/add";
            var json = JsonConvert.SerializeObject(note);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(route, content);

            return response;
        }
    }
}
