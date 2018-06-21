using System;
using Forecaster.Application.Input;

namespace Forecaster.Application.Output
{
    public class RendererFactory
    {
        public virtual IRenderer CreateFor(OutputFormat outputFormat)
        {
            switch (outputFormat)
            {
                case OutputFormat.Json:
                    throw new NotImplementedException();
                    break;
                case OutputFormat.Markdown:
                    throw new NotImplementedException();
                    break;
                default:
                    return new PrettyRenderer();
            }
        }
    }
}