using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GG.Portafolio.Site.Generic.Convert
{
    public class ConvertDateTime : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var valor = reader.GetString();
            if (valor.Contains("/Date(")) // formato Date(1514527200000-0600)
            {
                valor = valor.Replace("/Date(", "");
                valor = valor.Replace(")/", "");
                string[] miliseconds = valor.Split('-');
                DateTime fecha;

                if (miliseconds.Length > 1)
                {
                    fecha = ConvertToDatetime(miliseconds[0], "-" + miliseconds[1]);
                }
                else
                {
                    fecha = ConvertToDatetime(miliseconds[0]);
                }
                return fecha;
            }
            else if (valor.Contains("T") && valor.Contains("Z"))  // formato 2010-08-20T15:00:00Z
            {
                return DateTimeOffset.Parse(valor).UtcDateTime;
            }
            else // yyyy-mm-dd | yyyy-mm-dd hh:mm:ss
            {
                return DateTime.Parse(valor);
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {

        }

        private DateTime ConvertToDatetime(string strmiliseconds, string strmilisecondszh = "0")
        {
            DateTime unixEpoch = new(1970, 1, 1); // JavaScript usa la epoca de Unix of 1/1/1970
            double miliseconds = System.Convert.ToDouble(strmiliseconds);
            double milisecondszh = System.Convert.ToDouble(strmilisecondszh);
            DateTime dateTime = unixEpoch.AddMilliseconds(miliseconds).AddMilliseconds(milisecondszh).ToLocalTime();

            return dateTime;
        }
    }
}
