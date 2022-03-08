using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GG.Portafolio.Site.Generic.Convert
{
    public class ConvertBytes : JsonConverter<byte[]>
    {
        public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            byte[] sByteArray;

            if (reader.TokenType == JsonTokenType.String)
            {
                string a = JsonSerializer.Deserialize<string>(ref reader);
                sByteArray = System.Convert.FromBase64String(a);
            }
            else
            {
                sByteArray = JsonSerializer.Deserialize<byte[]>(ref reader);
            }

            byte[] value = new byte[sByteArray.Length];
            for (int i = 0; i < sByteArray.Length; i++)
            {
                value[i] = sByteArray[i];
            }

            return value;
        }

        public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var val in value)
            {
                writer.WriteNumberValue(val);
            }

            writer.WriteEndArray();
        }
    }
}
