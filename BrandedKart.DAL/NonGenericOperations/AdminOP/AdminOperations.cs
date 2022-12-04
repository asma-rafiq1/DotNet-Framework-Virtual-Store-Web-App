using AutoMapper;
using Brandedkart.DTO.Customer;
using Brandedkart.DTO.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BrandedKart.DAL.DataMapper;

namespace BrandedKart.DAL.NonGenericOperations.AdminOP
{
    public class AdminOperations : IAdminOperations
    {
        private readonly BrandedKartEntities _context;
        IMapper mapper = Mapping.Mapper;

        public AdminOperations(BrandedKartEntities context)
        {
            this._context = context;
        }
        public string[] GetUserRoles(string username)
        {

            return (from admin in _context.Admins
                    join roleMap
                    in _context.UserRolsMappings on admin.Admin_Id
                    equals roleMap.AdminId
                    join role in _context.Roles
                    on roleMap.RoleId equals role.Role_Id
                    where admin.Admin_Name.Equals(username)
                    select role.Role_Name).ToArray();

        }

        public async Task<AdminDTO> AdminLogin(AdminDTO admin)
        {
            Admin adminInfo = mapper.Map<Admin>(admin);
            return mapper.Map<AdminDTO>(await _context.Admins.Where(admindb => admindb.Admin_Name.Equals(adminInfo.Admin_Name) && admindb.Admin_Password.Equals(adminInfo.Admin_Password)).SingleAsync());
        }
    }
}
