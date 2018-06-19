namespace Musarium.Interfaces {
    public interface IQuestionTypeView {
        bool? ShowDialog();
        void BindDataContext(IQuestionTypeViewModel viewModel);
        void Hide();
    }
}
 