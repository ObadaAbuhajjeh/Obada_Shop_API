using System.Linq.Expressions;
using Obada_Shop.API.Model;

namespace Obada_Shop.API.ServicesLayer
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? Get(Expression<Func<Product, bool>> expression);
        Product Add(Product product);
        bool Remove(int id);
        bool Edit(int id, Product product);
        object Edit(int id);
    }
}
