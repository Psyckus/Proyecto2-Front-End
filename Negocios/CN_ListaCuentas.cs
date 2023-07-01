using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public partial class CN_ListaCuentas
    {
        [JsonProperty("NumeroCuenta")]
        public string NumeroCuenta { get; set; }

        [JsonProperty("Saldo")]
        public long Saldo { get; set; }

        [JsonProperty("TipoCuenta")]
        public string TipoCuenta { get; set; }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
        public override string ToString()
        {
            return ToJsonString();
        }
    }


    public partial class CN_ListaCuentas
    {
        public static List<CN_ListaCuentas> FromJson(string json) => JsonConvert.DeserializeObject<List<CN_ListaCuentas>>(json, CN_ListaCuentasConverter.Settings);
    }

    public static class CN_ListaCuentasSerialize
    {
        public static string ToJson(this List<CN_ListaCuentas> self) => JsonConvert.SerializeObject(self, CN_ListaCuentasConverter.Settings);
    }

    internal static class CN_ListaCuentasConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}


