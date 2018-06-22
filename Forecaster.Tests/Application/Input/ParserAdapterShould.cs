using FluentAssertions;
using Forecaster.Application.Input;
using Xunit;

namespace Forecaster.Tests.Application.Input
{
    public class ParserAdapterShould
    {
        [Theory]
        [InlineData("--samples 1 2 3 --forecast 5 --trials 1000 --outputFormat json", 
            new[] { 1.0, 2.0, 3.0}, 5, 1000, OutputFormat.Json)]
        [InlineData("-s 1 2 3 -f 5 -t 1000 -o markdown",
            new[] { 1.0, 2.0, 3.0 }, 5, 1000, OutputFormat.Markdown)]
        [InlineData("--samples 1 2 3 -f 5",
            new[] { 1.0, 2.0, 3.0 }, 5, 100000, OutputFormat.Pretty)]
        public void Parse_Syntactically_Valid_Options(string args, double[] expectedSamples, 
            int expectedForecast, int expectedTrials, OutputFormat expectedOutputFormat)
        {
            var adaptor = new ParserAdapter();
            var result = adaptor.Parse(args.Split(' '));

            result.IsSuccess.Should().BeTrue();
            result.SuccessResult.Samples.Should().BeEquivalentTo(expectedSamples);
            result.SuccessResult.Forecast.Should().Be(expectedForecast);
            result.SuccessResult.Trials.Should().Be(expectedTrials);
            result.SuccessResult.Output.Should().Be(expectedOutputFormat);
        }

        [Theory]
        [InlineData("")]
        [InlineData("-f 5 -t 1000")]
        [InlineData("--samples -forecast 5")]
        [InlineData("--samples 1 2 3 4 -forecast")]
        [InlineData("--samples 1 2 3 4 --forecast 5 --output xml")]
        public void Fail_To_Parse_Syntactically_Invalid_Options(string args)
        {
            var adaptor = new ParserAdapter();
            var result = adaptor.Parse(args.Split(' '));

            result.IsSuccess.Should().BeFalse();
        }
    }
}
