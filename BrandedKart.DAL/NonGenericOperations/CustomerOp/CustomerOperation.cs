using AutoMapper;
using Brandedkart.DTO.Customer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BrandedKart.DAL.DataMapper;

namespace BrandedKart.DAL.NonGenericOperations.CustomerOp
{
    public class CustomerOperation : ICustomerOperation
    {
        private readonly BrandedKartEntities _context;
        IMapper mapper = Mapping.Mapper;
        public CustomerOperation(BrandedKartEntities context)
        {
            _context = context;

        }
        public CustomerDTO AddCustomer(CustomerDTO customerInfo)
        {
            return mapper.Map<CustomerDTO>(_context.Customers.Add(mapper.Map<Customer>(customerInfo)));
        }


        public async Task<CustomerDTO> LoginCustomer(CustomerDTO customerInfo)
        {
            Customer customer = mapper.Map<Customer>(customerInfo);
            return mapper.Map<CustomerDTO>(await _context.Customers
                .SingleOrDefaultAsync(cust => cust.Email.Equals(customerInfo.Email) && cust.Customer_Password.Equals(customerInfo.Customer_Password)));
        }


        public async Task<bool> IsUserEmailAvailable(string Email)
        {
            return await _context.Customers.AnyAsync(cust => cust.Email.Equals(Email, StringComparison.OrdinalIgnoreCase));
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }


        //MS Recommended Approach to dipose instead of finalizer(Destructor)

        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
