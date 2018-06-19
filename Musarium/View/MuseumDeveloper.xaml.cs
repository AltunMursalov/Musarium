using Microsoft.Maps.MapControl.WPF;
using Musarium.Interfaces;
using System.Net;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;

namespace Musarium.View {
    public partial class MuseumDeveloper : Page, IMuseumDeveloperView {
        public MuseumDeveloper() {
            InitializeComponent();
        }

        public void BindDataContext(IMuseumDeveloperViewModel viewModel) {
            this.DataContext = viewModel;
        }

        public void SetCenter(Location center) {
            this.MuseumLocation.Center = center;
        }

        public MessageBoxResult ShowAlert(string text, string caption) {
            return MessageBox.Show(text, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        public void Hide() {
            this.Visibility = Visibility.Collapsed;
        }

        public void Show() {
            this.Visibility = Visibility.Visible;
        }
    }
}