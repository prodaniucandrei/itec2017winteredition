using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAppApi.Helpers.Filters
{
    public class CorsFilterAuth : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //            Console.WriteLine("CorsFilterAuth OnAuthorization");

            PopulateCorsHeaders(context.HttpContext);
            if (context.HttpContext.Request.Method.Equals("OPTIONS"))
            {
                //                Console.WriteLine("Finishing request because this is a OPTIONS request.");
                context.Result = new OkResult();
            }
            //            Console.WriteLine("CorsFilterAuth OnAuthorization Done");
        }


        public void PopulateCorsHeaders(HttpContext httpContext)
        {
            try
            {
                if (!httpContext.Response.HasStarted)
                {
                    PopCorsHeaders(httpContext);
                }
                else
                {
                    Console.WriteLine("CorsFilterAuth.PopulateCorsHeaders: Reponse already started.");
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("CorsFilterAuth.PopulateCorsHeaders Exception: " + exc.Message);
            }
        }

        public static void PopCorsHeaders(HttpContext httpContext)
        {
            var headers = httpContext.Request.Headers;

            //            Console.WriteLine(httpContext.HttpContext.Request.Method + "\r\n---------------\r\n" + JsonConvert.SerializeObject(headers));
            //            Console.WriteLine("Populating cors headers ...");
            var responseHeaders = httpContext.Response.Headers;
            if (headers.ContainsKey("Origin"))
            {
                var allowedOrigin = "*";
                allowedOrigin = headers["Origin"];
                responseHeaders.Remove("Access-Control-Allow-Origin");
                responseHeaders.Add("Access-Control-Allow-Origin", allowedOrigin);
            }

            if (headers.ContainsKey("Access-Control-Request-Method"))
            {
                var allowedMethod = "*";
                allowedMethod = headers["Access-Control-Request-Method"];
                responseHeaders.Remove("Access-Control-Allow-Methods");
                responseHeaders.Add("Access-Control-Allow-Methods", allowedMethod);
            }

            if (headers.ContainsKey("Access-Control-Request-Headers"))
            {
                var allowedHeader = "*";
                allowedHeader = headers["Access-Control-Request-Headers"];
                responseHeaders.Remove("Access-Control-Allow-Headers");
                responseHeaders.Add("Access-Control-Allow-Headers", allowedHeader);
            }

            responseHeaders.Remove("Access-Control-Allow-Credentials");
            responseHeaders.Add("Access-Control-Allow-Credentials", "true");

            if (httpContext.Request.Method.Equals("OPTIONS"))
            {
                //                Console.WriteLine("Forcing 200 statusCode because OPTIONS");
                httpContext.Response.StatusCode = 200;
            }
        }
    }
}
