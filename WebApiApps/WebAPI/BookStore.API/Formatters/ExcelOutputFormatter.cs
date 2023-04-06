using Domain.Models.Entity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace BookStore.API.Formatters;

public class ExcelOutputFormatter : OutputFormatter
{
    private string _excelMimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    public ExcelOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(_excelMimeType));
    }

    public override bool CanWriteResult(OutputFormatterCanWriteContext context)
            => context.ContentType.Equals(_excelMimeType);

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var httpContext = context.HttpContext;
        var serviceProvider = httpContext.RequestServices;
        var hostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

        var webRootPath = hostEnvironment.WebRootPath;
        var filename = "result.xlsx";
        var physicalPath = Path.Combine(webRootPath, "GeneratedStats", filename);
        var fileStream = new FileInfo(physicalPath).OpenRead();

        var content = new byte[fileStream.Length];
        fileStream.Read(content);

        await httpContext.Response.WriteAsync(Encoding.UTF8.GetString(content));
    }
}
