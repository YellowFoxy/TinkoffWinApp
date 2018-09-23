using Caliburn.Micro;
using TinkoffWinApp.Models;

namespace TinkoffWinApp.ViewModels
{
    public class ProductDetailViewModel : ViewModelBase
    {
        private Product _parameter;
        public Product Parameter
        {
            get { return _parameter; }
            set
            {
                if (value == _parameter)
                    return;
                _parameter = value;
                NotifyOfPropertyChange();
            }
        }

        public ProductDetailViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
