using Caliburn.Micro;
using System;
using TinkoffWinApp.Managers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace TinkoffWinApp.Controls
{
    public sealed partial class ImageLoadingControl
    {
        private readonly IImageManager _imageManager;

        private BitmapImage sourceImage;

        public ImageLoadingControl()
        {
            InitializeComponent();
            _imageManager = IoC.Get<IImageManager>();
        }

        public string ImageAddress
        {
            get { return (string)GetValue(ImageAddressProperty); }
            set { SetValue(ImageAddressProperty, value); }
        }

        public static readonly DependencyProperty ImageAddressProperty =
            DependencyProperty.Register("ImageAddress", typeof(string), typeof(ImageLoadingControl), new PropertyMetadata(null, OnImageAddressPropertyChanged));

        private static void OnImageAddressPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageLoadingControl imageLoadingControl = (ImageLoadingControl)d;

            if (string.IsNullOrEmpty(imageLoadingControl.ImageAddress))
                return;

            imageLoadingControl.StartLoading();
        }

        public void StartLoading()
        {
            ThisRing.IsActive = true;
            ThisRing.Visibility = Visibility.Visible;

            sourceImage = _imageManager.LoadBitmapImage(ImageAddress);
            ThisImage.ImageOpened += ThisImageImageOpened;
            ThisImage.ImageFailed += ThisImageImageFailed;
            ThisImage.Source = sourceImage;
            if (sourceImage.PixelHeight != 0 && sourceImage.PixelWidth != 0)
            {
                ThisRing.IsActive = false;
                ThisRing.Visibility = Visibility.Collapsed;
            }
        }

        private void ThisImageImageOpened(object sender, RoutedEventArgs e)
        {
            ThisRing.IsActive = false;
            ThisRing.Visibility = Visibility.Collapsed;
        }

        private void ThisImageImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            ThisRing.IsActive = false;
            ThisRing.Visibility = Visibility.Collapsed;
        }
    }
}
