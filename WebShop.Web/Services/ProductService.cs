using System.Text.Json;
using WebShop.Web.Models;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory clientFactory;
        private const string apiEndpoint = "/api/products/";
        private readonly JsonSerializerOptions _options;
        private ProductViewModel productVM;
        private IEnumerable<ProductViewModel> productVMs;
        public ProductService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

        }

        public Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            throw new NotImplementedException();
        }
        public Task<ProductViewModel> FindProductById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ProductViewModel> CreateProduct(ProductViewModel productVM)
        {
            throw new NotImplementedException();
        }
        public Task<ProductViewModel> UpdateProduct(ProductViewModel productVM)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteProductById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
