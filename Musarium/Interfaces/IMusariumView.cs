using System.Windows;

namespace Musarium.Interfaces {
    public interface IMusariumView {
        void BindDataContext(IMusariumViewModel viewModel);
        void Show();
        void Hide();
        void Close();
        void ShowAlert(string text, string caption);
        void SetFramesContent(IMuseumDeveloperView museumDeveloperView, IMuseumEditingView museumEditingView, IAddEditView addEditView);
    }
}
