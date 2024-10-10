using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Validation
{
    public class CustomDateValidation : ValidationAttribute
    {
        //geçerli tarih formatlarını tanımlayacağımız dizi
        private readonly string[] _validFormats;

        public CustomDateValidation(string[] validFormats)
        {
            _validFormats = validFormats;
        }

        // IsValid -> value ile gelecek değerin geçerliliğini kontrol eden method
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Date is required.");
            }

            // dateString -> value'yu string'e çeviriyor (çünkü tarihi string alıyoruz kontrol için)
            var dateString = value.ToString();

            // gelen tarihi format ile tek tek karşılaştırıyoruz
            foreach ( var format in _validFormats )
            {
                // DateTime.TryParseExact : girilen tarihi belirli formatla kontrol ediyor
                if(DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None,out _))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult($"Invalid date format. Accepted formats are: {string.Join(", ", _validFormats)}");
        }

    }
}
