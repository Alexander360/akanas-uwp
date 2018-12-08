using System;

using Akanas.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Akanas.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel => DataContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
