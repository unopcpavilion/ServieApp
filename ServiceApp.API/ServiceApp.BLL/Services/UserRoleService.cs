using ServiceApp.BLL.Interfaces;
using ServiceApp.DAL.Models;
using ServiceApp.DAL.Repository;

namespace ServiceApp.BLL.Services
{
    public class UserRoleService : BaseService<UsersRoles, int>, IUserRoleService
    {
        public UserRoleService(IRepository<UsersRoles, int> repository) : base(repository)
        {

        }
    }
}
