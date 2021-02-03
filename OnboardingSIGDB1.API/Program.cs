using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Microsoft.OpenApi.Writers;

namespace OnboardingSIGDB1.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();
            if (args.Length == 1 && args[0] == "generate")
            {
                var json = webHost.GenerateSwagger("v1", null);
                File.WriteAllText("swagger.json", json);
            }
            else
            {
                webHost.Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>();

        public static string GenerateSwagger(this IWebHost webhost, string docName, string basePath)
        {
            var sw = webhost.Services.GetRequiredService<ISwaggerProvider>();
            var doc = sw.GetSwagger(docName, null, basePath);

            using (var streamWriter = new StringWriter())
            {
                var writer = new OpenApiJsonWriter(streamWriter);
                doc.SerializeAsV3(writer);
                return streamWriter.ToString();
            }
        }
    }
}
