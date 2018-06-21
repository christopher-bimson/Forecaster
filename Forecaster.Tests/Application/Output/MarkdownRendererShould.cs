using Forecaster.Application.Output;
using Forecaster.Core.Model.Summary;
using NSubstitute;
using System;
using System.IO;
using Xunit;

namespace Forecaster.Tests.Application.Output
{
    public class MarkdownRendererShould
    {
        [Fact]
        public void Render_The_Forecast_As_Valid_Markdown_Table()
        {
            var writer = Substitute.For<TextWriter>();
            var summary = new[]
            {
                new Bucket(95, 35),
                new Bucket(80, 28),
                new Bucket(50, 17)
            };

            var expected =
                           "| Likelihood | At Least |" + Environment.NewLine +
                           "|------------|----------|" + Environment.NewLine +
                           "| 95         | 35       |" + Environment.NewLine +
                           "| 80         | 28       |" + Environment.NewLine +
                           "| 50         | 17       |" + Environment.NewLine;

            var markdownRenderer = new MarkdownRenderer(writer);
            markdownRenderer.Render(summary);

            writer.Received().Write(expected);
        }
    }
}