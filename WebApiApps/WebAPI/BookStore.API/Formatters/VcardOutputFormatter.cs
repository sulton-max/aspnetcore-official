using Domain.Models.Entity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace BookStore.API.Formatters
{
    public class VcardOutputFormatter : TextOutputFormatter
    {
        public VcardOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type) =>
            typeof(Book).IsAssignableFrom(type) || typeof(IEnumerable<Book>).IsAssignableFrom(type);

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;

            var logger = serviceProvider.GetRequiredService<ILogger<VcardOutputFormatter>>();
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<Book> books)
                books.ToList().ForEach(x => FormatVcard(buffer, x, logger));
            else if (context.Object != null)
                FormatVcard(buffer, (Book)context.Object, logger);

            await httpContext.Response.WriteAsync(buffer.ToString(), selectedEncoding);
        }

        private static void FormatVcard(StringBuilder buffer, Book book, ILogger logger)
        {
            buffer.AppendLine("BEGIN:VCARD");
            buffer.AppendLine("VERSION:2.1");
            buffer.AppendLine($"Name: {book.Name}");
            buffer.AppendLine($"Description {book.Description}");
            buffer.AppendLine("END:VCARD");

            logger.LogInformation("Writing {0} {1}", book.Name, book.Description);
        }
    }
}
