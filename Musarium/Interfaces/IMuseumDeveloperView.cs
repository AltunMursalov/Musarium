using Microsoft.Maps.MapControl.WPF;
using System.Windows;

namespace Musarium.Interfaces {
    public interface IMuseumDeveloperView {
        MessageBoxResult ShowAlert(string text, string caption);
        void BindDataContext(IMuseumDeveloperViewModel viewModel);
        void Hide();
        void Show();
        void SetCenter(Location center);
    }
}