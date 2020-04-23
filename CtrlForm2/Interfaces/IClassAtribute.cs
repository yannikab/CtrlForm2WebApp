using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.Interfaces
{
    public interface IClassAtribute
    {
        IReadOnlyList<string> ClassNames { get; }

        void AddClassName(string className);
    }
}
