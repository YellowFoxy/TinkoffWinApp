using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;

namespace TinkoffWinApp.Managers
{
    public interface IImageManager
    {
        BitmapImage LoadBitmapImage(string address);
    }

    public class ImageManager : IImageManager
    {
        private Dictionary<string, BitmapImage> _loadedImages;

        public ImageManager()
        {
            _loadedImages = new Dictionary<string, BitmapImage>();
        }

        public BitmapImage LoadBitmapImage(string address)
        {
            if (_loadedImages.ContainsKey(address))
                return _loadedImages[address];

            BitmapImage tempBitmapImage = new BitmapImage
            {
                UriSource = new Uri(address)
            };
            _loadedImages.Add(address, tempBitmapImage);
            return tempBitmapImage;
        }
    }
}
