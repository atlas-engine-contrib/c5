using Newtonsoft.Json;
using AtlasEngine.Modelling.C5.IO.Json;
using System.IO;

namespace AtlasEngine.Modelling.C5.Encryption
{
    public class EncryptedJsonReader
    {

        public EncryptedWorkspace Read(StringReader reader)
        {
            EncryptedWorkspace workspace = JsonConvert.DeserializeObject<EncryptedWorkspace>(
                reader.ReadToEnd(),
                new Newtonsoft.Json.Converters.StringEnumConverter(),
                new PaperSizeJsonConverter(),
                new EncryptionStrategyJsonConverter());

            return workspace;
        }

    }
}
