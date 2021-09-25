using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            if (productEntity.ProductId > 0)
                _db.Products.Update(productEntity);
            else
                _db.Products.Add(productEntity);

            await _db.SaveChangesAsync();
            var resultDto = _mapper.Map<ProductDto>(productEntity);
            return resultDto;
        }

        public async Task<bool> DeleteProduct(int idProduct)
        {
            try
            {
                var product = await _db.Products.FindAsync(idProduct);

                if (product == null)
                    return false;

                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _db.Products.FindAsync(id);
            var dto = _mapper.Map<ProductDto>(product);
            return dto;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _db.Products.ToListAsync();
            var dtos = _mapper.Map<List<ProductDto>>(products);
            return dtos;
        }
    }
}
