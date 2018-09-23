using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace TinkoffWinApp.Support
{
    public class LoadingHelper
    {
        private static LoadingHelper _instance;
        private Popup _loadingPopup;
        private ProgressRing _progressRing;

        private LoadingHelper()
        {
            var contentGrid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Height = Window.Current.Bounds.Height,
                Width = Window.Current.Bounds.Width,
                Background = new SolidColorBrush(Color.FromArgb(125, 0, 0, 0))
            };
            _progressRing = new ProgressRing
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = Window.Current.Bounds.Width / 4,
                Height = Window.Current.Bounds.Width / 4
            };
            contentGrid.Children.Add(_progressRing);
            _loadingPopup = new Popup()
            {
                HorizontalOffset = 0,
                VerticalOffset = 0,
                Height = Window.Current.Bounds.Height,
                Width = Window.Current.Bounds.Width,
                Child = contentGrid
            };
        }

        public static LoadingHelper GetInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new LoadingHelper();

                return _instance;
            }
        }

        public void StartLoadingIndicator()
        {
            _progressRing.IsActive = true;
            _loadingPopup.IsOpen = true;
        }

        public void StopLoadingIndicator()
        {
            _loadingPopup.IsOpen = false;
            _progressRing.IsActive = false;
        }
    }
}
