using CMSSample.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMSSample.DA.Repository
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private CMSSampleDAContext _context;

        public UserRolesRepository(CMSSampleDAContext cmssampledacontext)
        {
            this._context = cmssampledacontext;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<SelectListItem> GetAllUserRoles()
        {
           List<SelectListItem> UserRoles = _context.UserRoles.AsNoTracking()
                .OrderBy(n => n.RoleName)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.UserRoleID.ToString(),
                        Text = n.RoleName
                    }).ToList();

           var UserRolesdisp = new SelectListItem()
            {
                Value = null,
                Text = "--- select Role ---"
            };

            UserRoles.Insert(0, UserRolesdisp);
            return new SelectList(UserRoles, "Value", "Text");
        }

        public UserRoles GetUserRoleByRoleID(int RoleId)
        {
            return _context.UserRoles.Find(RoleId);
        }

    }
}
