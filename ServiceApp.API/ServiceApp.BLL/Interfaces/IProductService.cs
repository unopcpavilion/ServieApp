using ServiceApp.BLL.DTO;
using ServiceApp.DAL.Models;
using System.Threading.Tasks;

namespace ServiceApp.BLL.Interfaces
{
    public interface IProductService : IBaseService<Products, int>
    {
        Task<CreateProductViewModel> CreateNewProduct(CreateProductViewModel productmodel);
    }
}
