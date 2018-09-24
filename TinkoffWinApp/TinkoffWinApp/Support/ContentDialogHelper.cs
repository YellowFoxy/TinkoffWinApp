using Windows.UI.Xaml.Controls;

namespace TinkoffWinApp.Support
{
    public static class ContentDialogHelper
    {
        public static void ShowMessage(string title, string body)
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Ошибка",
                Content = "Произошла ошибка при получении данных, пожалуйста, попробуйте еще раз.",
                CloseButtonText = "Ok"
            };

            noWifiDialog.ShowAsync();
        }
    }
}
