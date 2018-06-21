using System;
using System.Collections.Generic;
using System.Text;

namespace Forecaster.Application
{
    public class RendererFactory
    {
        public virtual IRenderer Create()
        {
            return null;
        }
    }
}
