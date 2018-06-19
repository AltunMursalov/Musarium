namespace Musarium.Interfaces {
    public interface ILoginView {
        void BindDataContext(ILogInViewModel viewModel);
        void ShowAlert(string text, string caption);
    }
}