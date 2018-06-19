using System;
using System.Collections.Generic;
using System.Text;

namespace Forecaster.Core.Model
{
    public interface IRng
    {
        int Next(int exclusiveUpperBound);
    }
}
