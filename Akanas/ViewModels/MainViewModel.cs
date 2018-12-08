using System;

using Prism.Windows.Mvvm;
using Windows.UI;

namespace Akanas.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string title = "Akanas";
        public string Title
        {
            get { return title; }
            set { this.SetProperty(ref this.title, value); }
        }

        private Color foreground = Colors.White;
        public Color Foreground
        {
            get { return foreground; }
            set { this.SetProperty(ref this.foreground, value); }
        }

        private Color background = Color.FromArgb(255, 252, 107, 92);
        public Color Background
        {
            get { return background; }
            set { this.SetProperty(ref this.background, value); }
        }

        private string timerLabel ="25:00";
        public string TimerLabel
        {
            get { return timerLabel; }
            set { this.SetProperty(ref this.timerLabel, value); }
        }

        public MainViewModel()
        {
        }

        public void SetThemeToAkanas()
        {
            Title = "Akanas";
            Foreground = Colors.White;
            Background = Color.FromArgb(255, 252, 107, 92);
        }

        public void SetThemeToNas()
        {
            Title = "nas";
            Foreground = Colors.White;
            Background = Color.FromArgb(255, 102, 128, 184);
        }
    }
}
