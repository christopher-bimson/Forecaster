using System.Collections.Generic;

namespace Forecaster.Domain
{
    public interface IOutputWriter
    {
        void Write(IEnumerable<BandLikelihood> summary);
    }
}
