using System.Collections.Generic;
using System.IO;
using Forecaster.Core.Model.Summary;
using Newtonsoft.Json;

namespace Forecaster.Application.Output
{
    public class JsonRenderer : IRenderer
    {
        private readonly TextWriter writer;

        public JsonRenderer(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Render(IEnumerable<Bucket> summarizedForecast)
        {
            writer.Write(JsonConvert.SerializeObject(summarizedForecast, Formatting.Indented));
        }
    }
}
