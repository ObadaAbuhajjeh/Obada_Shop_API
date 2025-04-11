using System.Linq.Expressions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Obada_Shop.API.Data;
using Obada_Shop.API.DTOs.Requests;
using Obada_Shop.API.Model;

namespace Obada_Shop.API.ServicesLayer
{
    public class ProductService : IProductService
    {
        ApplicationDbContext _context;
        private object product;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Product Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product? Get(Expression<Func<Product, bool>> expression)
        {
            return _context.Products.FirstOrDefault(expression);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public bool Edit(int id, Product product)
        {
            Product? productInDb = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (productInDb == null) return false;
            
                product.Id = id;
                product.mainImg = productInDb.mainImg;
                _context.Products.Update(product);
                _context.SaveChanges();
                return true;
        }

        public bool Remove(int id)
        {
            Product? productInDb = _context.Products.Find(id);
            if (productInDb == null) return false;
            _context.Products.Remove(productInDb);
            _context.SaveChanges();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", productInDb.mainImg);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            return true;
        }

        public object Edit(int id)
        {
            throw new NotImplementedException();
        }
    }
}
