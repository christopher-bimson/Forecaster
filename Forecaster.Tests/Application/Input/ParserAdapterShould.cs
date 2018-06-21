using FluentAssertions;
using Forecaster.Application.Input;
using Xunit;

namespace Forecaster.Tests.Application.Input
{
    public class ParserAdapterShould
    {
        [Theory]
        [InlineData("--samples 1 2 3 --forecast 5 --trials 1000", 
            new[] { 1.0, 2.0, 3.0}, 5, 1000)]
        [InlineData("-s 1 2 3 -f 5 -t 1000",
            new[] { 1.0, 2.0, 3.0 }, 5, 1000)]
        [InlineData("--samples 1 2 3 -f 5",
            new[] { 1.0, 2.0, 3.0 }, 5, 10000)]
        public void Parse_Syntactically_Valid_Options(string args, double[] expectedSamples, 
            int expectedForecast, int expectedTrials)
        {
            var adaptor = new ParserAdapter();
            var result = adaptor.Parse(args.Split(' '));

            result.IsSuccess.Should().BeTrue();
            result.Success.Samples.Should().BeEquivalentTo(expectedSamples);
            result.Success.Forecast.Should().Be(expectedForecast);
            result.Success.TrialCount.Should().Be(expectedTrials);
        }

        [Theory]
        [InlineData("")]
        [InlineData("-f 5 -t 1000")]
        [InlineData("--samples -forecast 5")]
        [InlineData("--samples 1 2 3 4 -forecast")]
        [InlineData("--samples 1 2 3 4 -forecast wibble")]
        public void Fail_To_Parse_Syntactically_Invalid_Options(string args)
        {
            var adaptor = new ParserAdapter();
            var result = adaptor.Parse(args.Split(' '));

            result.IsSuccess.Should().BeFalse();
        }
    }
}
