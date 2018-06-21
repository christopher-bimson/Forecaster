using Forecaster.Application.Output;
using Forecaster.Core.Model.Summary;
using NSubstitute;
using System;
using System.IO;
using Xunit;

namespace Forecaster.Tests.Application.Output
{
    public class PrettyRendererShould
    {
        [Fact]
        public void Render_The_Forecast_In_A_Console_Compatible_Tabular_Format()
        {
            var writer = Substitute.For<TextWriter>();
            var summary = new[]
            {
                new Bucket(95, 35),
                new Bucket(80, 28),
                new Bucket(50, 17)
            };

            var expected = "+------------+----------+" + Environment.NewLine +
                           "| Likelihood | At Least |" + Environment.NewLine +
                           "+------------+----------+" + Environment.NewLine +
                           "| 95         | 35       |" + Environment.NewLine +
                           "+------------+----------+" + Environment.NewLine +
                           "| 80         | 28       |" + Environment.NewLine +
                           "+------------+----------+" + Environment.NewLine +
                           "| 50         | 17       |" + Environment.NewLine +
                           "+------------+----------+" + Environment.NewLine;

            var prettyRenderer = new PrettyRenderer(writer);
            prettyRenderer.Render(summary);
            writer.Received().Write(expected);
        }
    }
}