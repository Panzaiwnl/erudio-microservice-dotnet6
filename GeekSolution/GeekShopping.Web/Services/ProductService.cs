using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/Product";

        public async Task<ProductModel> CreateProduct(ProductModel model)
        {
           var response =  _client.PostAsJson(BasePath, model);
            if (response.Result.IsSuccessStatusCode)
            {
                return await response.Result.Content.ReadContentAs<ProductModel>();
            }
            else
            {
                throw new Exception("Something went wrong when calling API");
            }
        }

        public Task<bool> DeleteProductById(long id)
        {
            var response =  _client.DeleteAsync($"{BasePath}/{id}");
            if (response.Result.IsSuccessStatusCode)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public async Task<IEnumerable<ProductModel>> FindAllProducts()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.Content.ReadContentAs<List<ProductModel>>();
        }

        public async  Task<ProductModel> FindProductById(long id)
        {
            var response =  _client.GetAsync($"{BasePath}/{id}");
            return await response.Result.Content.ReadContentAs<ProductModel>();
        }

        public Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var response =  _client.PutAsJson(BasePath, model);
           if (response.Result.IsSuccessStatusCode)
            {
                return response.Result.Content.ReadContentAs<ProductModel>();
            }
            else
            {
                throw new Exception("Something went wrong when calling API");
            }
        }
    }
}
