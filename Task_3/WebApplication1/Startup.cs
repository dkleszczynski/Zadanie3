using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Task_2;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;


namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
                              
            app.Run(async (context) =>
            {
                HttpRequest hr = context.Request;

                string fileName = hr.Path;
                fileName = fileName.TrimStart(new char[] { '/' });

                if (fileName == "")
                {
                    await context.Response.WriteAsync("Podaj nazwe pliku w URL.");
                    return;
                }

                string viewCode = ViewGenerator.GetView(fileName);
                await context.Response.WriteAsync(viewCode, Encoding.Unicode);
                
            });
        }

        
    }
}
