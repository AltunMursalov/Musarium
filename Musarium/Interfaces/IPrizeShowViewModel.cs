using Musarium.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Musarium.Interfaces {
    public interface IPrizeShowViewModel {
        IPrizeShowView View { get; }
        ICommand CreatePrize { get; }
        Prize Prize { get; set; }
        ObservableCollection<Prize> Prizes { get; set; }
    }
}