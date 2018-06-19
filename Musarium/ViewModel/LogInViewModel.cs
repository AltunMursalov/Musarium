using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using System.Windows.Input;
using Autofac;
using System;

namespace Musarium.ViewModel
{
    public class LogInViewModel : NotifyableObject, ILogInViewModel
    {
        private IAccountService accountService;
        private IDataService dataService;
        public Museum Museum { get; set; }
        private AppData AppData = AppData.GetInstance();

        public LogInViewModel(ILoginView view)
        {
            accountService = AppData.Container.Resolve<IAccountService>();
            dataService = AppData.Container.Resolve<IDataService>();
            this.Museum = new Museum()
            {
                Login = "Bleschunov",
                Password = "pswd"
            };
            this.View = view;
            this.View.BindDataContext(this);
        }

        private ICommand login;

        public event EventHandler<EventArgs> Loginned;

        public ICommand LogIn
        {
            get
            {
                if (this.login is null)
                {
                    this.login = new RelayCommand(
                        (param) =>
                        {
                            var result = accountService.Login(Museum.Login, Museum.Password);
                            if (result == AuthenticationResult.IncorrectPassword)
                            {
                                View.ShowAlert("Incorrect password!", "Error");
                            }
                            else if (result == AuthenticationResult.IncorrectLogin)
                            {
                                View.ShowAlert("Incorrect login!", "Error");
                            }
                            else if (result == AuthenticationResult.IsNotExist)
                            {
                                View.ShowAlert("This user is not exist!", "Error");
                            }
                            else if (result == AuthenticationResult.InvalidConnection)
                            {
                                View.ShowAlert("The database is not exist! Also cheсk connection!", "Error");
                            }
                            else
                            {
                                Loginned?.Invoke(this, new EventArgs());
                            }
                        },
                        (param) => { return (Museum.Login?.Length ?? 0) > 3 && (Museum.Password?.Length ?? 0) > 3; }
                    );
                }
                return this.login;
            }
        }

        public ILoginView View { get; }
    }
} 