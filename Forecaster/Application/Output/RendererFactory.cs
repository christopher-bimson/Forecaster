using Forecaster.Application.Input;
using System.IO;

namespace Forecaster.Application.Output
{
    public class RendererFactory
    {
        private readonly TextWriter writer;

        public RendererFactory(TextWriter writer)
        {
            this.writer = writer;
        }

        public virtual IRenderer CreateFor(OutputFormat outputFormat)
        {
            switch (outputFormat)
            {
                case OutputFormat.Json:
                    return new JsonRenderer(writer);
                case OutputFormat.Markdown:
                    return new MarkdownRenderer(writer);
                default:
                    return new PrettyRenderer(writer);
            }
        }
    }
}