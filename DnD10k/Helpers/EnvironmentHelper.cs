using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD10k.Helpers
{
    public static class EnvironmentHelper
    {
        public static bool IsDevelopment()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return environment == "Development";
        }
    }
}

