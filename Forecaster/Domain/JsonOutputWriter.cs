using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Forecaster.Domain
{
    internal class JsonOutputWriter : IOutputWriter
    {
        private readonly TextWriter _writer;

        internal JsonOutputWriter(TextWriter textWriter)
        {
            _writer = textWriter;
        }

        public void Write(IEnumerable<BandLikelihood> summary)
        {
            _writer.Write(JsonConvert.SerializeObject(summary, Formatting.Indented));
        }
    }
}
