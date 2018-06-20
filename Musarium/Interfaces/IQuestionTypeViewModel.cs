using System.Windows.Input;

namespace Musarium.Interfaces {
    public interface IQuestionTypeViewModel {
        IQuestionTypeView View { get; }
        ICommand Choose { get; }
    }
} 