using System;
using System.IO;
using BetterConsoleTables;

namespace Forecaster.Application.Output
{
    public class PrettyRenderer : TableRenderer
    {
        public PrettyRenderer(TextWriter writer) : base (writer)
        {
        }

        protected override TableConfiguration GetTableConfig()
        {
            return TableConfiguration.MySql();
        }
    }
}
