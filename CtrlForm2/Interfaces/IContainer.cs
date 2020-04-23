using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserControls.CtrlForm2.Interfaces
{
    public interface IContainer : IContainable
    {
        IReadOnlyList<IContainable> Contents { get; }

        void Add(IContainable c);

        bool Remove(IContainable c);
    }
}
