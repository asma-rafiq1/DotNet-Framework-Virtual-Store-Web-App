using Brandedkart.DTO.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandedKart.DAL.GenericOperations
{
    public interface IUnitOfWork
    {
        IGenericOperation<Customer> CustomerRepository { get; }
        IGenericOperation<Category> CategoryRepo { get; }

        IGenericOperation<Product> ProductRepo { get; }
        void Save();
        void Dispose();
    }
}
