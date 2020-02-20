using ServiceApp.BLL.Interfaces;
using ServiceApp.DAL.Models;
using ServiceApp.DAL.Repository;

namespace ServiceApp.BLL.Services
{
    public class RoleService : BaseService<Roles, int>, IRoleService
    {
        public RoleService(IRepository<Roles, int> repository) : base(repository)
        { }
    }
}
