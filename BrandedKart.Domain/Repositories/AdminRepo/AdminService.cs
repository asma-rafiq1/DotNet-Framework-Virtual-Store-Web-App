using Brandedkart.DTO.User;
using BrandedKart.DAL;
using BrandedKart.DAL.NonGenericOperations.AdminOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandedKart.Domain.Repositories.AdminRepo
{
    public class AdminService : IAdminService
    {
        private readonly IAdminOperations _admin;
        private BrandedKartEntities _context = new BrandedKartEntities();

        public AdminService()
        {
            _admin = new AdminOperations(_context);
        }
        public string[] GetUserRoles(string username)
        {
            return _admin.GetUserRoles(username);
        }

        public async Task<AdminDTO> AdminLogin(AdminDTO admin)
        {
            return await _admin.AdminLogin(admin);
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
