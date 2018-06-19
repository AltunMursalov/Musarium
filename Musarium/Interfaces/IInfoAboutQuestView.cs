namespace Musarium.Interfaces {
    public interface IInfoAboutQuestView {
        void BindDataContext(IInfoAboutQuestViewModel viewModel);
        void ShowAlert(string text, string caption);
        void Show();
    }
}
