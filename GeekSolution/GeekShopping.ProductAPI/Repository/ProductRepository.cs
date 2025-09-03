using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRespository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public ProductRepository(MySQLContext context, IMapper mapper)
        {
             _context = context;
             _mapper = mapper;

        }
        public async Task<ProductVO> Create(ProductVO vo)
        {

            Product products = _mapper.Map<Product>(vo);
            _context.Products.Add(products);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(products);  
        }

        public async Task<bool> Delete(long id)
        {
           try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null) return false;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            return _mapper.Map<ProductVO>(product);
        }


        public async Task<ProductVO> Update(ProductVO vo)
        {
            Product products = _mapper.Map<Product>(vo);
            _context.Products.Update(products);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(products);
        }
    }
}
