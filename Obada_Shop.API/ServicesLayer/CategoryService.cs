using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Obada_Shop.API.Data;
using Obada_Shop.API.Model;

namespace Obada_Shop.API.ServicesLayer
{
    public class CategoryService : ICategoryService
    {
        ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Category Add(Category category)
        {
            _context.categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public bool Edit(int id, Category category)
        {
            Category? categoryInDb = _context.categories.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (categoryInDb == null) return false;
            category.Id = id;
            _context.categories.Update(category);
            _context.SaveChanges();
            return true;
        }

        public Category? Get(Expression<Func<Category, bool>> expression)
        {
            return _context.categories.FirstOrDefault(expression);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.categories.ToList();
        }

        public bool Remove(int id)
        {
            Category? categoryInDb = _context.categories.Find(id);
            if (categoryInDb == null) return false;
            _context.categories.Remove(categoryInDb);
            _context.SaveChanges();
            return true;
        }
    }
}
