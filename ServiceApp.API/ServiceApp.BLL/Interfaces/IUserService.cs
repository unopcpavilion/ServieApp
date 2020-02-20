using ServiceApp.BLL.DTO;
using ServiceApp.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceApp.BLL.Interfaces
{
    public interface IUserService : IBaseService<Users, int>
    {
        Task<UserViewModel> Authenticate(string username, string password);
        Task<IEnumerable<UserViewModel>> GetAllUsers();
        Task<RegisterUserVeiwModel> Register(RegisterUserVeiwModel model);

    }
}
