using Microsoft.Maps.MapControl.WPF;
using Musarium.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace Musarium.View {
    public partial class MuseumEditing : Page, IMuseumEditingView {
        public MuseumEditing() {
            InitializeComponent();
            Visibility = Visibility.Collapsed;
        }

        public void BindDataContext(IMuseumEditingViewModel viewModel) {
            this.DataContext = viewModel;
          
        }

        public void SetCenter(Location center) {
            this.MuseumLocation.Center = center;
        }

        public void Hide() {
            this.Visibility = Visibility.Collapsed;
        }

        public void Show() {
            this.Visibility = Visibility.Visible;
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption);
        }

        private void MuseumLocation_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            e.Handled = true;
            this.MuseumLocation.Children.Clear();
            Point mousePosition = e.GetPosition(MuseumLocation);
            Location pinLocation = this.MuseumLocation.ViewportPointToLocation(mousePosition);
            push.GetValue(MapLayer.PositionProperty);
            push.SetValue(MapLayer.PositionProperty, pinLocation);
            push.Location = pinLocation;
            this.MuseumLocation.Center = push.Location;
            this.MuseumLocation.Children.Add(push);
        }
    }
}