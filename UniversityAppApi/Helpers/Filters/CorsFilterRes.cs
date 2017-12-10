using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAppApi.Helpers.Filters
{
    public class CorsFilterRes : IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //            Console.WriteLine("CorsFilterRes OnResourceExecuting...");


            PopulateCorsHeaders(context.HttpContext);
            if (context.HttpContext.Request.Method.Equals("OPTIONS"))
            {
                //                Console.WriteLine("Finishing request because this is a OPTIONS request.");
                context.Result = new OkResult();
            }
            //            Console.WriteLine("CorsFilterRes OnResourceExecuting Done");
        }

        public void PopulateCorsHeaders(HttpContext httpContext)
        {
            try
            {
                if (!httpContext.Response.HasStarted)
                {
                    CorsFilterAuth.PopCorsHeaders(httpContext);
                }
                else
                {
                    Console.WriteLine("CorsFilterRes.PopulateCorsHeaders: Reponse already started.");
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("CorsFilterRes.PopulateCorsHeaders Exception: " + exc.Message);
                Console.WriteLine(exc.StackTrace);
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //            Console.WriteLine("CorsFilterRes OnResourceExecuted");
        }
    }
}
