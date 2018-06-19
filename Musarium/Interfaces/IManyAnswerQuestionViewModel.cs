using System.Windows.Input;

namespace Musarium.Interfaces {
    public interface IManyAnswerQuestionViewModel {
        IManyAnswerQuestionView View { get; }
        ICommand NewAnswer { get; }
    }
}