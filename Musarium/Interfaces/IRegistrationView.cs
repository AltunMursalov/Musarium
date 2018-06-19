using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musarium.Interfaces {
    public interface IRegistrationView {
        void BindDataContext(IRegistrationViewModel viewModel);
        void ShowAlert(string text, string caption);
    }
}
