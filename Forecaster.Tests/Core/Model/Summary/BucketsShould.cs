using FluentAssertions;
using Forecaster.Core.Model.Summary;
using Xunit;

namespace Forecaster.Tests.Core.Model.Summary
{
    public class BucketsShould
    {
        [Fact]
        public void Not_Bother_To_Record_Things_That_Are_Not_Gonna_Happen()
        {
            var buckets = new Buckets();

            buckets.Add(0, 50);

            buckets.Should().BeEmpty();
        }

        [Fact]
        public void Record_The_Highest_Forecase_For_A_Given_Likelihood()
        {
            var buckets = new Buckets();

            buckets.Add(75, 50);
            buckets.Add(75, 60);

            buckets.Should().ContainSingle(b => b.Forecast == 60);
        }
    }
}
