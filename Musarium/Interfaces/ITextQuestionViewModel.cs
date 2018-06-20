using Musarium.Model;

namespace Musarium.Interfaces {
    public interface ITextQuestionViewModel {
        Question Question { get; set; }
        ITextQuestionView View { get; }
        Answer Answer { get; set; }
    }
}
