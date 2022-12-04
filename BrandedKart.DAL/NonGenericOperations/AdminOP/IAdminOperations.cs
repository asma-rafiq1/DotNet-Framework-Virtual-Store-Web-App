using Brandedkart.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandedKart.DAL.NonGenericOperations.AdminOP
{
     public interface IAdminOperations
    {
        string[] GetUserRoles(string username);

        Task<AdminDTO> AdminLogin(AdminDTO admin);
    }
}
