using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Obada_Shop.API.Data;
using Obada_Shop.API.Model;

namespace Obada_Shop.API.ServicesLayer
{
    public class BrandService : IBrandService
    {
        ApplicationDbContext _context;
        public BrandService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Brand Add(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return brand;
        }

        public bool Edit(int id, Brand brand)
        {
            Brand? brandInDb = _context.Brands.AsNoTracking().FirstOrDefault(b => b.Id == id);
            if (brandInDb == null) return false;
            brand.Id = id;
            _context.Brands.Update(brand);
            _context.SaveChanges();
            return true;
        }

        public Brand? Get(Expression<Func<Brand, bool>> expression)
        {
            return _context.Brands.FirstOrDefault(expression);
        }

        public IEnumerable<Brand> GetAll()
        {
            return _context.Brands.ToList();
        }

        public bool Remove(int id)
        {
            Brand? brandInDb = _context.Brands.Find(id);
            if (brandInDb == null) return false;
            _context.Brands.Remove(brandInDb);
            _context.SaveChanges();
            return true;
        }
    }
}
