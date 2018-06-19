using Musarium.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Musarium.Interfaces {
    public interface IMuseumDeveloperViewModel {
        IMuseumDeveloperView View { get; }
        ObservableCollection<Quest> Quests { get; }
        ObservableCollection<Prize> Prizes { get; }
        Museum Museum { get; set; }
        ICommand DeleteQuest { get; }
    }
} 