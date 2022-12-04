using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BrandedKart.DAL.GenericOperations
{
    internal class GenericOperations<TEntity> : IGenericOperation<TEntity> where TEntity : class
    {
        private readonly BrandedKartEntities _context;
        private readonly DbSet<TEntity> dbset;

        public GenericOperations(BrandedKartEntities context)
        {
            this._context = context;
            this.dbset = context.Set<TEntity>();
        }


        public virtual void Add(TEntity obj)
        {
            dbset.Add(obj);
        }

        public virtual void Delete(TEntity obj)
        {
            _context.Entry<TEntity>(obj).State = EntityState.Deleted;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbset.ToList();
        }

        public virtual TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(TEntity obj)
        {
            _context.Entry<TEntity>(obj).State = EntityState.Modified;
        }
    }
}
