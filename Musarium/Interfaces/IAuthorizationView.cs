namespace Musarium.Interfaces {
    public interface IAuthorizationView {
        void BindDataContext(IAuthorizationViewModel viewModel);
        void Hide();
        void Show();
        void SetFramesContent(ILoginView login, IRegistrationView registration);
    }
} 