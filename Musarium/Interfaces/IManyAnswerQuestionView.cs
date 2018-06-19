namespace Musarium.Interfaces {
    public interface IManyAnswerQuestionView {
        void BindDataContext(IManyAnswerQuestionViewModel viewModel);
        bool? ShowDialog();
        void ShowAlert(string text, string caption);
    }
}
 