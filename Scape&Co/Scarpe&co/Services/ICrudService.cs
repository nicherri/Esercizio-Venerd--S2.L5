using System.Collections.Generic;

namespace Scarpe_co.Services
{
    public interface ICrudService<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
