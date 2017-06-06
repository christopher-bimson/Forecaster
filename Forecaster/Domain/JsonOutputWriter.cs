using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

namespace Forecaster.Domain
{
    internal class JsonOutputWriter : IOutputWriter
    {
        private readonly TextWriter _textWriter;
        private readonly JsonSerializerSettings _settings =
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

        internal JsonOutputWriter(TextWriter textWriter)
        {
            _textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
        }

        public void Write(IEnumerable<BandLikelihood> summary)
        {
            _textWriter.Write(JsonConvert.SerializeObject(summary, _settings));
        }
    }
}
