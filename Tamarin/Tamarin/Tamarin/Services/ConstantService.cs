using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamarin.Services
{
    public static class ConstantService
    {
        private static Uri apiUrl = new Uri("http://192.168.1.138:51182/api/");
        //private static Uri apiUrl = new Uri("http://10.0.2.2:51182/api/");

        public static Uri GetUrl()
        {
            return apiUrl;
        }
    }
}
