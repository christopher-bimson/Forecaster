using System;
using System.Collections.Generic;
using System.Text;

namespace Forecaster.Core.Model.Trial
{
    public interface IRng
    {
        int Next(int exclusiveUpperBound);
    }
}
