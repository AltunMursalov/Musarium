using Musarium.Model;

namespace Musarium.Interfaces {
    public interface ICreateQuestsViewModel {
        ICreateQuestsView View { get; }
        Question Question { get; set; }
    }
} 