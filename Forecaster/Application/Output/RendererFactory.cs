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
                    return new JsonRenderer();
                case OutputFormat.Markdown:
                    return new MarkdownRenderer();
                default:
                    return new PrettyRenderer();
            }
        }
    }
}