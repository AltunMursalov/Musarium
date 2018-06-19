namespace Musarium.Interfaces {
    public interface IAddEditView {
        void BindDataContext(IAddEditViewModel viewModel);
        void ShowAlert(string text, string caption);
        void SetFramesContext(ITaskInfoAboutQuestView aboutQuestView, ICreateQuestsView createQuestsView, IPrizeShowView prizeShowView);
        void Hide();
        void ChangedButtonToDone();
        void Show();
    }
}
