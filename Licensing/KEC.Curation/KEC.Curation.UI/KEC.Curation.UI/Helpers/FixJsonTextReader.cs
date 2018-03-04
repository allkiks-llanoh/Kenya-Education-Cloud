using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KEC.Curation.UI.Helpers
{
    public class FixedJsonTextReader : JsonTextReader
    {
        public FixedJsonTextReader(TextReader reader) : base(reader) { }

        public override int? ReadAsInt32()
        {
            try
            {
                return base.ReadAsInt32();
            }
            catch (JsonReaderException)
            {
                if (TokenType == JsonToken.PropertyName)
                    SetToken(JsonToken.None);
                throw;
            }
        }
    }
}