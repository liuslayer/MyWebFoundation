using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebFoundation.DB
{
    public interface IScopeTransaction : IDisposable
    {
        void Complete();
    }
}
