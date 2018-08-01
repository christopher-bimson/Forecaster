using Forecaster.Application.Output;
using Forecaster.Core.Model.Summary;
using NSubstitute;
using System;
using System.IO;
using Xunit;

namespace Forecaster.Tests.Application.Output
{
    public class CsvRendererShould
    {
        [Fact]
        public void Render_The_Forecast_In_A_Format_Suitable_For_Excel_Jockeys()
        {
            var writer = Substitute.For<TextWriter>();
            var summary = new[]
            {
                new Bucket(95, 35),
                new Bucket(80, 28),
                new Bucket(50, 17)
            };

            var expectedOutput = "Forecast,Likelihood" + Environment.NewLine +
                                 "35,95" + Environment.NewLine +
                                 "28,80" + Environment.NewLine +
                                 "17,50" + Environment.NewLine;

            var csvRenderer = new CsvRenderer(writer);
            csvRenderer.Render(summary);

            writer.Received().Write(expectedOutput);
        }
    }
}
