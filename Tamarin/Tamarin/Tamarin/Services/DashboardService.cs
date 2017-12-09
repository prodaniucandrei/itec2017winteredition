using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tamarin.Services
{
    class DashboardService
    {
        private static HttpClient client;

        public static async Task<HttpResponseMessage> GetDashboard()
        {
            client = new HttpClient();
            client.BaseAddress = ConstantService.GetUrl();
            client.MaxResponseContentBufferSize = 256000;

            var studentId = App.Current.Properties["id"] as string;

            var route = "dashboard/getdashboard" + "/" + studentId;

            var token = App.Current.Properties["token"] as string;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(route);

            return response;
        }
    }
}
