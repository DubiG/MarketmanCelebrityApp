using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celebrities.DAL
{
    public interface IRepository<E> where E : class
    {
        List<E> GetAll();
        E Find(int id);
        int Insert(E entity);
        void Delete(int id);
        void Update(E entity);
        void Reset();
    }
}
