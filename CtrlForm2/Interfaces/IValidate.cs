using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControls.CtrlForm2.Interfaces
{
    public interface IValidate<T>
    {
        Func<T, string> ValidationError { get; }

        bool IsValid { get; }

        Action<T> ActionInvalid { get; }
    }
}
