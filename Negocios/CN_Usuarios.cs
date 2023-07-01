using System;
using System.Collections.Generic;

using System.Globalization;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Formatting = Newtonsoft.Json.Formatting;

namespace Negocios
{
    public partial class CN_Usuarios
    {

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("nombreCliente")]
        public string nombreCliente { get; set; }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
        public override string ToString()
        {
            return ToJsonString();
        }

    }

    public partial class CN_Usuarios
    {
        public static List<CN_Usuarios> FromJson(string json) => JsonConvert.DeserializeObject<List<CN_Usuarios>>(json, CN_UsuariosConverter.Settings);
    }

    public static class CN_UsuariosSerialize
    {
        public static string ToJson(this CN_Usuarios self) => JsonConvert.SerializeObject(self, CN_UsuariosConverter.Settings);
    }

    internal static class CN_UsuariosConverter
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