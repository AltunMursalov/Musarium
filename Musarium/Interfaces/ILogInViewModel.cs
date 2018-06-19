using Musarium.Model;
using System;
using System.Windows.Input;

namespace Musarium.Interfaces {
    public interface ILogInViewModel {
        ICommand LogIn { get; }
        Museum Museum { get; set; }
        ILoginView View { get; }
        event EventHandler<EventArgs> Loginned;
    }
}