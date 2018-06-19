using Musarium.Interfaces;
using System.Windows.Input;
using Autofac;
using Musarium.Common;
using Musarium.Model;
using System;

namespace Musarium.ViewModel {
    public class RegistrationViewModel : NotifyableObject, IRegistrationViewModel {
        private IAccountService accountService;
        private AppData AppData = AppData.GetInstance();
        public RegisterData Museum { get; set; }

        public IRegistrationView View { get; private set; }
        public RegistrationViewModel(IRegistrationView view) {
            Museum = new RegisterData();
            this.View = view;
            this.View.BindDataContext(this);
            this.accountService = AppData.Container.Resolve<IAccountService>();
        }

        private ICommand registration;

        public ICommand Registration {
            get {
                if (this.registration is null) {
                    this.registration = new RelayCommand(
                        (param) => {
                            var result = accountService.Registration(Museum);
                            if (result == RegistrationResult.ShortPassword) {
                                View.ShowAlert("Password is too short!", "Error");
                            } else if (result == RegistrationResult.LoginIsExist) {
                                View.ShowAlert("This login is exist!", "Error");
                            } else if (result == RegistrationResult.PasswordsIsNotMatch) {
                                View.ShowAlert("Passwords are not match!", "Error");
                            } else if (result == RegistrationResult.InvalidConnection) {
                                View.ShowAlert("The database is not exist! Also chek connection!", "Error");
                            } else {
                                Museum.City = String.Empty;
                                Museum.Login = String.Empty;
                                Museum.ValidatePassword = String.Empty;
                                Museum.Password = String.Empty;
                                this.View.ShowAlert("Successfuly registered!", "Confirmed!");
                            }
                        },
                        (param) => {
                            return (Museum.Login?.Length ?? 0) > 0 && (Museum.City?.Length ?? 0) > 0 &&
                                   (Museum.Password?.Length ?? 0) > 6 && (Museum.ValidatePassword?.Length ?? 0) > 6;
                        }
                    );
                }
                return this.registration;
            }
        }
    }
}
