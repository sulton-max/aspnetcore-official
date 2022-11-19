using Domain.Models.Entity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace BookStore.API.Formatters
{
    public class VcardInputFormatter : TextInputFormatter
    {
        public VcardInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanReadType(Type type)
            => type == typeof(Book);

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;

            var logger = serviceProvider.GetRequiredService<ILogger<VcardInputFormatter>>();

            using var reader = new StreamReader(httpContext.Request.Body, encoding);
            var name = string.Empty;

            try
            {
                await ReadLineAsync("BEGIN:VCARD", reader, context);
                await ReadLineAsync("VERSION:", reader, context);

                name = await ReadLineAsync("Name:", reader, context);

                var book = new Book { Name = name };
                return await InputFormatterResult.SuccessAsync(book);
            }
            catch
            {
                return await InputFormatterResult.FailureAsync();
            }
        }

        private static async Task<string> ReadLineAsync(string value, StreamReader reader, InputFormatterContext context)
        {
            var line = await reader.ReadLineAsync();

            if (line is null || !line.StartsWith(value))
                throw new Exception();

            return line;
        }
    }
}
