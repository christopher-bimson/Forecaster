using System;

namespace Forecaster.Core.Model.Trial
{
    public class RandomRng : IRng
    {
        private readonly Random random;

        public RandomRng()
        {
            random = new Random();
        }

        public int Next(int exclusiveUpperBound)
        {
            return random.Next(exclusiveUpperBound);
        }
    }
}
