using Autofac;
using System.Windows;
using Musarium.Common;
using Musarium.Interfaces;

namespace Musarium {
    public partial class App : Application {
        private AppData AppData = AppData.GetInstance();
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            var viewModel = AppData.Container.Resolve<IAuthorizationViewModel>();
            var mainView = viewModel.View as Window;
            this.MainWindow = mainView;
            this.MainWindow.Show();
        }
    }
} 