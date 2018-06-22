using System.IO;
using BetterConsoleTables;

namespace Forecaster.Application.Output
{
    public class MarkdownRenderer : TableRenderer
    {
        public MarkdownRenderer(TextWriter writer) : base(writer)
        {
        }

        protected override TableConfiguration GetTableConfig()
        {
            return TableConfiguration.Markdown();
        }
    }
}
