using FluentAssertions;
using Forecaster.Application.Input;
using Forecaster.Application.Output;
using System;
using Xunit;

namespace Forecaster.Tests.Application.Output
{
    public class RendererFactoryShould
    {
        [Theory]
        [InlineData(OutputFormat.Pretty, typeof(PrettyRenderer))]
        [InlineData(OutputFormat.Json, typeof(JsonRenderer))]
        [InlineData(OutputFormat.Markdown, typeof(MarkdownRenderer))]
        public void Create_The_Correct_Type_Of_Renderer(OutputFormat outputFormat, 
            Type expectedRenderer)
        {
            var factory = new RendererFactory();

            var renderer = factory.CreateFor(outputFormat);

            renderer.Should().BeOfType(expectedRenderer);
        }
    }
}
