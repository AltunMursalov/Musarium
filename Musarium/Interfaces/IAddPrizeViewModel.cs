using System.Windows.Input;

namespace Musarium.Interfaces {
    public interface IAddPrizeViewModel {
        IAddPrizeView View { get; }
        ICommand Save { get; }
    }
} 