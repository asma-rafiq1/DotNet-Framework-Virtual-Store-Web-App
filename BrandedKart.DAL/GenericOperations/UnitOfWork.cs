using Brandedkart.DTO.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandedKart.DAL.GenericOperations
{
    public class UnitOfWork : IUnitOfWork
    {
        private BrandedKartEntities dbcontext = new BrandedKartEntities();
        private IGenericOperation<Customer> customerRepository;

        public IGenericOperation<Customer> CustomerRepository
        {
            get
            {
                if (this.customerRepository == null)
                {
                    this.customerRepository = new GenericOperations<Customer>(dbcontext);
                }
                return customerRepository;
            }
        }

        private IGenericOperation<Category> categoryRepo;

        public IGenericOperation<Category> CategoryRepo
        {
            get
            {
                if (this.categoryRepo == null)
                {
                    this.categoryRepo = new GenericOperations<Category>(dbcontext);
                }
                return categoryRepo;
            }
        }

        private IGenericOperation<Product> productRepo;

        public IGenericOperation<Product> ProductRepo
        {
            get
            {
                if (this.productRepo == null)
                {
                    this.productRepo = new GenericOperations<Product>(dbcontext);
                }
                return productRepo;
            }
        }

        public void Save()
        {
            dbcontext.SaveChanges();
        }
        private bool isDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    dbcontext.Dispose();
                    dbcontext = null;
                }
            }
            this.isDisposed = true;
        }

    }
}

