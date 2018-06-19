namespace Musarium.Interfaces
{
    public interface ITaskInfoAboutQuestView
    {
        void BindDataContext(ITaskInfoAboutQuestViewModel viewModel);
        void Show();
        void Hide();
        void ShowAlert(string text, string caption);
    }
}
