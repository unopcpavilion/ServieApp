using System;
using System.Collections.Generic;
using System.Text;
using ServiceApp.DAL.Models;
using ServiceApp.DAL.Repository;
using ServiceApp.BLL.Interfaces;
using System.Threading.Tasks;
using ServiceApp.BLL.DTO;
using System.Linq;

namespace ServiceApp.BLL.Services
{
   public class ProductService: BaseService<Products,int>, IProductService
    {
        public ProductService(IRepository<Products, int> repository): base(repository)
        {

        }

        public async Task<CreateProductViewModel> CreateNewProduct(CreateProductViewModel productmodel)
        {
            using (_repository.BeginTransaction())
            {
                try
                {
                    var product = GetAll(x => x.Name.ToLower().Equals(productmodel.Name) && x.Status == true).FirstOrDefault();
                    if(product == null)
                    {
                        var data = new Products
                        {
                            Name = productmodel.Name,
                            Price = productmodel.Price,
                            Status = productmodel.Status
                        };
                        var result = await Create(data);
                        productmodel.Id = result.Id;
                        _repository.CommitTransaction();
                        return productmodel;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    _repository.RollbackTransaction();
                    return null;
                }
            }
        }
    }
}
