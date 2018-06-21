using System;
using System.Collections.Generic;
using System.Text;

namespace Forecaster.Application.Output
{
    public class RendererFactory
    {
        public virtual IRenderer Create()
        {
            return null;
        }
    }
}
