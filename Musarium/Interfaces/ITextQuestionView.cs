namespace Musarium.Interfaces {
    public interface ITextQuestionView {
        void BindDataContext(ITextQuestionViewModel viewModel);
        bool? ShowDialog();
        void Hide();
        void Clear();
        void ShowAlert(string text, string caption);
    }
}