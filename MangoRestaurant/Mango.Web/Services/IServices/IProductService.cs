﻿using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IProductService : IBaseService
    {
        Task<T> GetAllProductsAsync<T>(string token);
        Task<T> GetProductByIDAsync<T>(int id, string token);
        Task<T> CreateProductAsync<T>(ProductDto dto, string token);
        Task<T> UpdateProductAsync<T>(ProductDto dto, string token);
        Task<T> DeleteProductAsync<T>(int id, string token);

    }
}
