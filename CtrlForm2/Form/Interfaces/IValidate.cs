using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlForm2.Form.Interfaces
{
    public interface IValidate<T>
    {
        Func<T, string> Validator { get; set; }

        Action<T> ActionInvalid { get; set; }

        bool IsValid { get; }

        string ValidationMessage { get; }
    }
}
