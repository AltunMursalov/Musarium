using Microsoft.Maps.MapControl.WPF;
using Musarium.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Musarium.View {
    /// <summary>
    /// Interaction logic for Musarium.xaml
    /// </summary>
    public partial class Musariume : Window, IMusariumView {
        public Musariume() {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

        public void BindDataContext(IMusariumViewModel viewModel) {
            this.DataContext = viewModel;
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        //private void Window_Unloaded(object sender, RoutedEventArgs e) {
        //    Application.Current.Shutdown();
        //}

        public void SetFramesContent(IMuseumDeveloperView museumDeveloperView, IMuseumEditingView museumEditingView, IAddEditView addEditView) {
            this.MuseumDeveloper.Content = museumDeveloperView;
            this.AddEditQuest.Content = addEditView;
            this.MuseumEditing.Content = museumEditingView;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}