using System.Collections.Generic;
using Forecaster.Core.Model.Summary;

namespace Forecaster.Application.Output
{
    public interface IRenderer
    {
        void Render(IEnumerable<Bucket> summarizedForecast);
    }
}