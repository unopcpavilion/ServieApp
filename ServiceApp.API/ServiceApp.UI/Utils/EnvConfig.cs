using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApp.UI.Utils
{
    public class EnvConfig
    {
        public static string APIUrl { get; set; }
        public static string CompanyName { get; set; }

        public EnvConfig(Microsoft.Extensions.Configuration.IConfiguration config)
        {
            APIUrl = config["APIUrl"];
        }
    }
}
