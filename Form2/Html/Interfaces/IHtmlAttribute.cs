using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form2.Html.Interfaces
{
    public interface IHtmlAttribute
    {
        string Name { get; }

        bool IsSet { get; }
    }
}
