using FluentAssertions;
using Forecaster.Application.Output;
using Forecaster.Core.Model.Summary;
using Newtonsoft.Json;
using NSubstitute;
using System.IO;
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

            writer.Received().Write(Arg.Is<string>(s => DeserializesTo(s, summary)));
        }

        private bool DeserializesTo(string s, Bucket[] summary)
        {
            var deserializedSummary = JsonConvert.DeserializeObject<Bucket[]>(s);
            deserializedSummary.Should().BeEquivalentTo(summary);
            return true;
        }
    }
}