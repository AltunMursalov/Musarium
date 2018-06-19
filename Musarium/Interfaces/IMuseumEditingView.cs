using Microsoft.Maps.MapControl.WPF;

namespace Musarium.Interfaces {
    public interface IMuseumEditingView {
        void ShowAlert(string text, string caption);
        void BindDataContext(IMuseumEditingViewModel viewModel);
        void Hide();
        void Show();
        void SetCenter(Location center);
    }
}