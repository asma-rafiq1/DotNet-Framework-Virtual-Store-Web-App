using Brandedkart.DTO.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandedKart.Domain.Repositories.CustomerRepo
{
    public interface ICustomerService
    {
        CustomerDTO AddCustomer(CustomerDTO customer);
        Task<CustomerDTO> LoginCustomer(CustomerDTO customer);
        Task<bool> IsUserEmailAvailable(string Email);
        void Dispose();
    }
}
