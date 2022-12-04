using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BrandedKart.DAL.GenericOperations
{
    public interface IGenericOperation<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        void Update(TEntity obj);
        void Delete(TEntity obj);
        void Add(TEntity obj);
    }
}
