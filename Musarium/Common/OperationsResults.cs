using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musarium.Common {
    public enum AuthenticationResult {
        IncorrectPassword,
        IncorrectLogin,
        IsNotExist,
        InvalidConnection,
        Successful
    }
     
    public enum RegistrationResult {
        LoginIsExist,
        PasswordsIsNotMatch,
        ShortPassword,
        InvalidConnection,
        Successful
    }
}
