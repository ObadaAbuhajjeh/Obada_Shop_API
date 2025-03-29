using System.Linq.Expressions;
using Obada_Shop.API.Model;

namespace Obada_Shop.API.ServicesLayer
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category? Get(Expression <Func<Category, bool>> expression);
        Category Add(Category category);
        bool Edit(int id , Category category);
        bool Remove(int id);
    }
}
