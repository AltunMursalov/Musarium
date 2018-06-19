using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musarium.Interfaces
{
    public interface ITextQuestionView
    {
        void BindDataContext(ITextQuestionViewModel viewModel);
        bool? ShowDialog();
        void Hide();
    }
}
 