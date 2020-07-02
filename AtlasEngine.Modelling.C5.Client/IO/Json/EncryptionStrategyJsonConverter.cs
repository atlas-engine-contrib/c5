using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AtlasEngine.Modelling.C5.Encryption;
using System;
using System.Reflection;

namespace AtlasEngine.Modelling.C5.IO.Json
{
    internal class EncryptionStrategyJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(EncryptionStrategy).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject item = JObject.Load(reader);
            string type = item["type"].Value<string>();
            if (type == "aes")
            {
                return item.ToObject<AesEncryptionStrategy>();
            }
            else
            {
                throw new NotSupportedException("The encryption strategy type of " + type + " is not supported");
            }
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("This operation is not implemented");
        }
    }
}
