using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UniversityAppApi.Helpers
{
    public class ConfigHelpers
    {
        public string GetConnectionString(IConfigurationRoot config)
        {
            var env = Utills.EnvironmentName;
            var connString = config[$"ConnectionStrings:{env}"];
            if (string.IsNullOrEmpty(connString))
            {
                var exception = "Connection string not found for environment: " + env;
                Console.WriteLine(exception);
                throw new Exception(exception);
            }
            if (!connString.EndsWith(";"))
            {
                connString += ";";
            }
            connString += "Persist Security Info=True;Convert Zero Datetime=True;charset=utf8";
            return connString;
        }

        public object GetConfigField(string field)
        {
            var appSettingsPath = "config.json";
            if (!File.Exists(appSettingsPath))
            {
                var asmPath = typeof(Program).GetTypeInfo().Assembly.Location;
                var asmDir = Path.GetDirectoryName(asmPath);
                appSettingsPath = Path.Combine(asmDir, appSettingsPath);
            }
            var str = "";
            using (
                var sr = new StreamReader(new FileStream(appSettingsPath, FileMode.Open)))
            {
                str = sr.ReadToEnd();
            }
            //            Console.WriteLine(str);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(str);
            var path = field.Split(':');
            for (var i = 0; i < path.Length; i++)
            {
                var fieldName = path[i];

                if (!dict.ContainsKey(fieldName))
                {
                    Console.WriteLine($"Not containing key '{field}'");
                    return null;
                }
                if (i == path.Length - 1)
                {
                    return dict[fieldName];
                }
                dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(
                    JsonConvert.SerializeObject(dict[fieldName]));
            }
            return null;
        }
    }
}
