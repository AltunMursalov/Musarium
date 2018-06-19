namespace Musarium.Interfaces {
    public interface IAuthorizationViewModel {
        ILogInViewModel LogInViewModel { get; }
        IRegistrationViewModel RegistrationViewModel { get; }
        IAuthorizationView View { get; }
    }
}
