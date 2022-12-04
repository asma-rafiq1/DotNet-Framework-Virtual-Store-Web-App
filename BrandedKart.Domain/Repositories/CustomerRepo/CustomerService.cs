using Brandedkart.DTO.Customer;
using Brandedkart.DTO.User;
using BrandedKart.DAL;
using BrandedKart.DAL.NonGenericOperations.CustomerOp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BrandedKart.Domain.Repositories.CustomerRepo
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerOperation _customer;
        private BrandedKartEntities _context = new BrandedKartEntities();
        public CustomerService()
        {
            _customer = new CustomerOperation(_context);
        }
        public CustomerDTO AddCustomer(CustomerDTO customer)
        {
            var CustomerInfo = _customer.AddCustomer(customer);
            _customer.SaveChanges();
            return CustomerInfo;
        }

        public async Task<CustomerDTO> LoginCustomer(CustomerDTO customer)
        {
            return await _customer.LoginCustomer(customer);

        }


        public async Task<bool> IsUserEmailAvailable(string Email)
        {
            return await _customer.IsUserEmailAvailable(Email);
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
                    _context.Dispose();
                    _context = null;
                }
            }
            this.isDisposed = true;
        }




    }
}
