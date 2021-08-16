using FlexHR.DataAccess.Concrete.EntityFrameworkCore.Context;
using FlexHR.DataAccess.Interface;
using FlexHR.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexHR.DataAccess.Concrete.EntityFrameworkCore.Repository
{
    public class EfAppUserRepository : EfGenericRepository<AppUser>, IAppUserDal
    {
        private readonly FlexHRContext _context;
        public EfAppUserRepository(FlexHRContext context) : base(context)
        {
            _context = context;
        }
        public List<AppRole> GetAppRolesByStaffId(int id)
        {
          
            return _context.UserRoles.Join(_context.Users, userRoles => userRoles.UserId, users => users.Id, (resultUserRole, resultUser) => new
            {
                userRole = resultUserRole,
                user = resultUser
            }).Join
             (_context.Roles, userRoles => userRoles.userRole.RoleId, role => role.Id, (resultUserRole, resultRole) => new
             {
                 user = resultUserRole.user,
                 userRole = resultUserRole.userRole,
                 roles = resultRole
             }).Where(p => p.user.StaffId==id).Select(p => new AppRole()
             {
                 Description=p.roles.Description,
                 AuthorizeTypeGeneralSubTypeId=p.roles.AuthorizeTypeGeneralSubTypeId,
                 NormalizedName=p.roles.NormalizedName,
                 Name=p.roles.Name,
                 ConcurrencyStamp=p.roles.ConcurrencyStamp,
                 Id=p.roles.Id,
                 IsActive=p.roles.IsActive              
             }).ToList();
        
        }
        public List<AppRole> GetAppRolesByUserId(int id)
        {

            return _context.UserRoles.Join(_context.Users, userRoles => userRoles.UserId, users => users.Id, (resultUserRole, resultUser) => new
            {
                userRole = resultUserRole,
                user = resultUser
            }).Join
             (_context.Roles, userRoles => userRoles.userRole.RoleId, role => role.Id, (resultUserRole, resultRole) => new
             {
                 user = resultUserRole.user,
                 userRole = resultUserRole.userRole,
                 roles = resultRole
             }).Where(p => p.user.Id == id).Select(p => new AppRole()
             {
                 Description = p.roles.Description,
                 AuthorizeTypeGeneralSubTypeId = p.roles.AuthorizeTypeGeneralSubTypeId,
                 NormalizedName = p.roles.NormalizedName,
                 Name = p.roles.Name,
                 ConcurrencyStamp = p.roles.ConcurrencyStamp,
                 Id = p.roles.Id,
                 IsActive = p.roles.IsActive
             }).ToList();

        }
    }
}
