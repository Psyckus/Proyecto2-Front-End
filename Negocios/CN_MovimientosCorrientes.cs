using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public partial class CN_MovimientosCorrientes
    {
        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("CuentaCorriente")]
        public string CuentaCorriente { get; set; }

        [JsonProperty("Fecha")]
        public DateTimeOffset Fecha { get; set; }

        [JsonProperty("TipoMovimiento")]
        public string TipoMovimiento { get; set; }

        [JsonProperty("Monto")]
        public long Monto { get; set; }

        [JsonProperty("TipoTransaccion")]
        public string TipoTransaccion { get; set; }

        [JsonProperty("Identificador")]
        public string Identificador { get; set; }

        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("Canal")]
        public string Canal { get; set; }
    }



    public partial class CN_MovimientosCorrientes
    {
        public static List<CN_MovimientosCorrientes> FromJson(string json) => JsonConvert.DeserializeObject<List<CN_MovimientosCorrientes>>(json, CN_MovimientosCorrientesConverter.Settings);
    }

    public static class CN_MovimientosCorrientesSerialize
    {
        public static string ToJson(this List<CN_MovimientosCorrientes> self) => JsonConvert.SerializeObject(self, CN_MovimientosCorrientesConverter.Settings);
    }

    internal static class CN_MovimientosCorrientesConverter
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
