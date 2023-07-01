using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Negocios
{
    public partial class CN_MovimientosAhorro
    { 
        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("Cuenta")]
        public string Cuenta { get; set; }

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


    public partial class CN_MovimientosAhorro
    {
        public static List<CN_MovimientosAhorro> FromJson(string json) => JsonConvert.DeserializeObject<List<CN_MovimientosAhorro>>(json, CN_MovimientosAhorroConverter.Settings);
    }

    public static class gggSerialize
    {
        public static string ToJson(this List<CN_MovimientosAhorro> self) => JsonConvert.SerializeObject(self, CN_MovimientosAhorroConverter.Settings);
    }

    internal static class CN_MovimientosAhorroConverter
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
