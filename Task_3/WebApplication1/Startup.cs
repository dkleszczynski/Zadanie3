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

                string filePath = "";
                bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

                if (isWindows)
                {
                    DriveInfo[] drives = DriveInfo.GetDrives();
                    
                    foreach (DriveInfo drive in drives)
                    {
                        if (drive.IsReady)
                        {
                            filePath = drive.Name + fileName;
                            break;
                        }
                    }
                }
                else
                    filePath = "/home/" + fileName;

                var sb = new StringBuilder();

                sb.Append(@"<html>
                                <head>
                                    <style>
                                        table, th, td {	  border: 1px solid;
				                                          border-collapse: collapse;
				                                          margin-bottom: 20px; }

                                        table { table-layout: fixed;
		                                        width: 100%; }
                                        th { width: 17%; }
                                        td { word-wrap: break-word; }
                                    </style>
                                </head>
                                <body>
                                    <h3>Plik o nazwie " + fileName + "</h3>");
                


                FileManager fm = new FileManager();
                FileData file = fm.GetFile(filePath);

                
                FileAttributes fileAttributes = File.GetAttributes(file.FileInfo.FullName);
                bool isHidden = false;

                if ((fileAttributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    isHidden = true;

                sb.Append(@"<table >
                                <tr>
                                    <th>Ścieżka:</th>
                                    <td>" + file.FileInfo.FullName + @"</td>
                                </tr>
                                <tr>
                                    <th>Utworzenie:</th>
                                    <td>" + file.FileInfo.CreationTime + @"</td>
                                </tr>
                                <tr>
                                    <th>Ostatni dostęp:</th>
                                    <td>" + file.FileInfo.LastAccessTime + @"</td>
                                </tr>
                                <tr>
                                    <th>Ostatni zapis:</th>
                                    <td>" + file.FileInfo.LastWriteTime + @"</td>
                                </tr>
                                <tr>
                                    <th>Plik jest ukryty:</th>
                                    <td>" + isHidden + @"</td>
                                </tr>
                                <tr>
                                    <th>Tylko do odczytu:</th>
                                    <td>" + file.FileInfo.IsReadOnly + @"</td>
                                </tr>
                            </table>
                        </ body >
                     <html >");





                await context.Response.WriteAsync(sb.ToString(), Encoding.Unicode);
                
            });
        }

        
    }
}
