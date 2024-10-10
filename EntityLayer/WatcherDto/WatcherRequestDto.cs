using EntityLayer.Enums;
using EntityLayer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace EntityLayer.WatcherDto
{
    public class WatcherRequestDto
    {
        [Required(ErrorMessage = "Start date is required.")]
        [JsonConverter(typeof(DateFormatConverter))]
        //[CustomDateValidation(new[] { "dd/MM/yyyy", "yyyy/MM/dd" })]
        public DateTime startDate { get; set; }


        [Required(ErrorMessage = "End date is required.")]
        [JsonConverter(typeof(DateFormatConverter))]
        // [CustomDateValidation(new[] { "dd/MM/yyyy", "yyyy/MM/dd" })]
        public DateTime endDate { get; set; }
        public FilterTypeEnum Type { get; set; }

    }
    public class DateFormatConverter : JsonConverter<DateTime>
    {
        private readonly string _format = "yyyy-MM-dd";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), _format, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}
