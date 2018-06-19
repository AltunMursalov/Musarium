using Musarium.Model;

namespace Musarium.Interfaces {
    public interface ITaskInfoAboutQuestViewModel {
        ITaskInfoAboutQuestView View { get; }
        Quest Quest { get; set; }
    }
} 