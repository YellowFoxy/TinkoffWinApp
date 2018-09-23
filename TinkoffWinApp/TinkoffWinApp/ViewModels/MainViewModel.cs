using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;
using TinkoffWinApp.Extensions;
using TinkoffWinApp.Managers;
using TinkoffWinApp.Models;

namespace TinkoffWinApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IProductManager _productManager;

        private List<Product> _collection;
        public List<Product> Collection
        {
            get { return _collection; }
            set
            {
                if (value == _collection)
                    return;
                _collection = value;
                NotifyOfPropertyChange();
            }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange();
                ProductSelected(_selectedProduct);
            }
        }

        public MainViewModel(INavigationService navigationService, 
            IProductManager productManager) : base(navigationService)
        {
            _productManager = productManager;
            
        }

        protected override async void OnInitialize()
        {
            Collection = await LoadTask(() => _productManager.GetProductsList());
            if (!Collection.IsEmpty())
            {
                //ProductSelected(Collection.FirstOrDefault());
            }
        }

        private void ProductSelected(Product selectedProduct)
        {
            if (selectedProduct == null)
                return;

            NavigationService.NavigateToViewModel<ProductDetailViewModel>(selectedProduct);
        }
    }
}
