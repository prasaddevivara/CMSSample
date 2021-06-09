using CMSSample.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMSSample.DA.Repository
{
    public interface IUserRolesRepository: IDisposable
    {
        IEnumerable<SelectListItem> GetAllUserRoles();
        UserRoles GetUserRoleByRoleID(int RoleId);
    }
}
