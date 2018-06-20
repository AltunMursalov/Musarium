using Musarium.Model;
using System.Collections.ObjectModel;

namespace Musarium.Interfaces {
    public interface ICreateQuestsViewModel {
        ICreateQuestsView View { get; }
        ObservableCollection<Question> Questions { get; set; }
        ObservableCollection<Answer> Answers { get; set; }
    }
} 