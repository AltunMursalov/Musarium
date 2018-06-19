using Musarium.Common;
using Musarium.Model;

namespace Musarium.Interfaces {
    public interface IAccountService {
        AuthenticationResult Login(string login, string password);
        void EditProfile(Museum museum);
        RegistrationResult Registration(RegisterData Museum);
    }
} 