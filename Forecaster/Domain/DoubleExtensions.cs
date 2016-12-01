using System;

namespace Forecaster.Domain
{
    public static class DoubleExtensions
    { 
        public static int FloorToNearest(this double d, int nearest)
        {
            return (int)Math.Floor(d / nearest) * nearest;
        }
    }
}