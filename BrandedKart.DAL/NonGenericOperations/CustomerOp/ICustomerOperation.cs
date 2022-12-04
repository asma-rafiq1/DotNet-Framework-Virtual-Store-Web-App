using Brandedkart.DTO.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandedKart.DAL.NonGenericOperations.CustomerOp
{
    public interface ICustomerOperation
    {
        CustomerDTO AddCustomer(CustomerDTO cust);
        Task<CustomerDTO> LoginCustomer(CustomerDTO cust);

        Task<bool> IsUserEmailAvailable(string Email);
        void SaveChanges();
    }
}
