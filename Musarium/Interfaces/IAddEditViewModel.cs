using Musarium.Model;
using System.Windows.Media;

namespace Musarium.Interfaces {
    public interface IAddEditViewModel {
        IAddEditView View { get; }

        Brush FirstStepBG { get; set; }
        Brush SecondStepBG { get; set; }
        Brush ThirdStepBG { get; set; }
    }
}
