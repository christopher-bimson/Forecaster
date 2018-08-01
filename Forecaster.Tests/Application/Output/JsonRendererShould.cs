using FluentAssertions;
using Forecaster.Application.Output;
using Forecaster.Core.Model.Summary;
using Newtonsoft.Json;
using NSubstitute;
using System.IO;
using System.Collections.Generic;
using Xunit;

namespace Forecaster.Tests.Application.Output
{
    public class JsonRendererShould
    {
        [Fact]
        public void Render_The_Forecast_In_Valid_JSON()
        {
            var writer = Substitute.For<TextWriter>();
            var summary = new[]
            {
                new Bucket(95, 35),
                new Bucket(80, 28),
                new Bucket(50, 17)
            };

            var jsonRenderer = new JsonRenderer(writer);
            jsonRenderer.Render(summary);

            writer.Received().Write(Arg.Is<string>(json => DeserializesTo(json, summary)));
        }

        private bool DeserializesTo(string json, IEnumerable<Bucket> summary)
        {
            var deserializedSummary = JsonConvert.DeserializeObject<IEnumerable<Bucket>>(json);
            deserializedSummary.Should().BeEquivalentTo(summary);
            return true;
        }
    }
}