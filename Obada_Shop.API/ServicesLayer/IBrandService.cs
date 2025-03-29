using System.Linq.Expressions;
using Obada_Shop.API.Model;

namespace Obada_Shop.API.ServicesLayer
{
    public interface IBrandService
    {
        IEnumerable<Brand> GetAll();
        Brand? Get(Expression<Func<Brand, bool>> expression);
        Brand Add(Brand brand);
        bool Edit(int id, Brand brand);
        bool Remove(int id);
    }
}
