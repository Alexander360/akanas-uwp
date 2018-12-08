using System;

using Akanas.ViewModels;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Akanas.Views
{
    public sealed partial class MainPage : Page, IDisposable
    {
        private MainViewModel ViewModel => DataContext as MainViewModel;

        private DispatcherTimer timer = null;
        private TimeSpan remainingTime = new TimeSpan(0, 0, 25, 0, 900);
        private DateTime exitTime;
        private bool counting = false;
        private bool interval = false;

        private MediaPlayer mediaPlayer;

        public MainPage()
        {
            InitializeComponent();

            // タイマーの準備
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(100);
            this.timer.Tick += Timer_Tick;

            // メディアプレーヤーの準備
            this.mediaPlayer = new MediaPlayer();
        }

        private void Timer_Tick(object sender, object e)
        {
            remainingTime = exitTime - DateTime.Now;

            if (remainingTime.TotalMilliseconds < 1000)
            {
                SwitchAkanas();

                if (interval)
                {
                    remainingTime = new TimeSpan(0, 0, 5, 00, 900);
                }
                else
                {
                    remainingTime = new TimeSpan(0, 0, 25, 00, 900);
                    if (counting)
                    {
                        DoAkanas();
                    }
                }
                exitTime = DateTime.Now.AddTicks(remainingTime.Ticks);
            }

            UpdateText();
        }

        private void DoAkanas()
        {
            if (counting == false)
            {
                exitTime = DateTime.Now.AddTicks(remainingTime.Ticks);
                this.timer.Start();
            }
            else
            {
                this.timer.Stop();
            }

            counting = !counting;
        }

        private void SwitchAkanas()
        {
            if (interval == false)
            {
                SetThemeToNas();
                PlayAudio("warning1.mp3");
            }
            else
            {
                SetThemeToAkanas();
                PlayAudio("decision1.mp3");
            }

            interval = !interval;
        }

        private void UpdateText()
        {
            var min = Math.Floor(remainingTime.TotalMinutes);
            var sec = Math.Floor(remainingTime.TotalSeconds) % 60;

            //await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            //{
                ViewModel.TimerLabel = $"{min:00}:{sec:00}";
            //});
        }

        private void SetThemeToAkanas()
        {
            // タイトルバーを更新
            var app = App.Current as App;
            app.SetTitleBarColorToAkanas();
            // ページ要素を更新
            ViewModel.SetThemeToAkanas();
        }

        private void SetThemeToNas()
        {
            // タイトルバーを更新
            var app = App.Current as App;
            app.SetTitleBarColorToNas();
            // ページ要素を更新
            ViewModel.SetThemeToNas();
        }

        private void PlayAudio(string filename)
        {
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/{filename}"));
            mediaPlayer.Play();
        }

        private void Rectangle_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            PlayAudio("cursor1.mp3");

            DoAkanas();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // マネージド状態を破棄します (マネージド オブジェクト)。
                    mediaPlayer.Dispose();
                }

                disposedValue = true;
            }
        }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
        }
        #endregion
    }
}
