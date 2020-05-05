using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CtrlForm2.Form.Items.Input;

namespace CtrlForm2.Form.Interfaces
{
    public interface IValidate<T> where T : FormItemInput
    {
        Func<T, string> Validator { get; set; }

        Action<T> ActionInvalid { get; set; }

        bool IsValid { get; }

        string ValidationMessage { get; }
    }
}
