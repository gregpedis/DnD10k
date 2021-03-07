using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DnD10k
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var webHost = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(
                webBuilder =>
                {
                    var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
                    webBuilder.UseStartup<Startup>();

                    if (isLinux)
                    {
                        var socketPath = Environment.GetEnvironmentVariable("SOCKET_PATH");
                        webBuilder.UseLibuv().UseKestrel(options => options.ListenUnixSocket(socketPath));
                    }
                });

            return webHost;
        }
    }
}

