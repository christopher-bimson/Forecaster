using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;

namespace Forecaster.Domain
{
    internal class JsonOutputWriter : IOutputWriter
    {
        private readonly TextWriter _writer;
        private readonly JsonSerializerSettings _settings =
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

        internal JsonOutputWriter(TextWriter textWriter)
        {
            _writer = textWriter;
        }

        public void Write(IEnumerable<BandLikelihood> summary)
        {
            _writer.Write(JsonConvert.SerializeObject(summary, _settings));
        }
    }
}
