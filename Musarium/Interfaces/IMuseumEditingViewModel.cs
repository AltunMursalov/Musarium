using Musarium.Model;

namespace Musarium.Interfaces {
    public interface IMuseumEditingViewModel {
        IMuseumEditingView View { get; }
        Museum Museum { get; set; }
    }
}