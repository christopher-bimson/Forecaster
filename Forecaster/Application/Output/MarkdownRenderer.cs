using System;
using System.Collections.Generic;
using System.IO;
using BetterConsoleTables;
using Forecaster.Core.Model.Summary;

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
