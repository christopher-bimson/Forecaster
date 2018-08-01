using System;

namespace Forecaster.Core.Model.Trial
{
    public class SystemRandomRng : IRng
    {
        private readonly Random random;

        public SystemRandomRng()
        {
            random = new Random();
        }

        public int Next(int exclusiveUpperBound)
        {
            return random.Next(exclusiveUpperBound);
        }
    }
}
