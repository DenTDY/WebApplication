using Entities.DataTransferObjects.House;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace WebApplication.Formatter
{
    public class CsvOutputFormatterHouse : TextOutputFormatter
    {
        public CsvOutputFormatterHouse()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(HouseDto).IsAssignableFrom(type) ||
                typeof(IEnumerable<HouseDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<HouseDto>)
            {
                foreach (var company in (IEnumerable<HouseDto>)context.Object)
                {
                    FormatCsv(buffer, company);
                }
            }
            else
            {
                FormatCsv(buffer, (HouseDto)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatCsv(StringBuilder buffer, HouseDto house)
        {
            buffer.AppendLine($"{house.Id},\"{house.Name},\"{house.AddressAndNumberFloors},\"{house.YearConstruction}");
        }
    }
}
