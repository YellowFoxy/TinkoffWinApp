using System.Collections.Generic;
using System.Threading.Tasks;
using TinkoffWinApp.Extensions;
using TinkoffWinApp.Models;

namespace TinkoffWinApp.Managers
{
    public interface IProductManager
    {
        Task<List<Product>> GetProductsList(bool needUpdate = false);
    }

    public class ProductManager : IProductManager
    {
        private readonly IRequestManager _requestManager;

        private List<Product> _loadedProducts;

        public ProductManager(IRequestManager requestManager)
        {
            _requestManager = requestManager;
        }

        public async Task<List<Product>> GetProductsList(bool needUpdate = false)
        {
            if (!needUpdate && !_loadedProducts.IsEmpty())
                return _loadedProducts;

            var result = await _requestManager.LoadProducts();

            if (!result.Success || result.Result?.Value.IsEmpty() == true)
                return new List<Product>();

            _loadedProducts = result.Result?.Value;
            return _loadedProducts;//new List<Product> { result.Result?.Value[0],result.Result?.Value[1], result.Result?.Value[2] };
        }
    }
}
