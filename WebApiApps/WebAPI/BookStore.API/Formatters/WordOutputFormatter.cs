using Domain.Models.Entity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace BookStore.API.Formatters
{
    public class WordOutputFormatter : OutputFormatter
    {
        private string _wordMimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        public WordOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(_wordMimeType));
        }


        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
            => context.ContentType.Equals(_wordMimeType);

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;
            var hostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

            var buffer = new StringBuilder();

            var webRootPath = hostEnvironment.WebRootPath;
            var filename = "result.docx";
            var physicalPath = Path.Combine(webRootPath, "GeneratedStats", filename);
            var fileStream = new FileInfo(physicalPath).OpenRead();

            var content = new byte[fileStream.Length];
            fileStream.Read(content);

            await httpContext.Response.WriteAsync(Encoding.UTF8.GetString(content)); 
        }
    }
}
