namespace Musarium.Interfaces {
    public interface ICreateQuestsView {
        void BindDataContext(ICreateQuestsViewModel viewModel);
        void ShowAlert(string text, string caption);
        void Show();
        void Hide();
    }
}