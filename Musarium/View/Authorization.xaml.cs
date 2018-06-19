using Musarium.Interfaces;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Musarium.View {
    public partial class Authorization : Window, IAuthorizationView, INotifyPropertyChanged {
        public Thickness To { get; set; } = new Thickness(500, 0, 0, 0);
        public Thickness From { get; set; } = new Thickness(0, 0, 0, 0);

        public Authorization() {
            InitializeComponent();
        }

        public void BindDataContext(IAuthorizationViewModel viewModel) {
            this.DataContext = viewModel;
           
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption);
        }

        private void Close_Program(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Right)
                return;
            this.DragMove();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e) {
            var a = To;
            To = From;
            From = a;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("To"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("From"));
        }

        public void SetFramesContent(ILoginView login, IRegistrationView register) {
            this.AddReg.Content = register;
            this.Main.Content = login;
        }
    }
}