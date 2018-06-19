using Musarium.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Musarium.Interfaces {
    public interface IMusariumViewModel {
        IMusariumView View { get; }
    }
}