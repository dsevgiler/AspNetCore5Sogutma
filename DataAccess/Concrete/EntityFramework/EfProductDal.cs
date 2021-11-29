using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, UcsanContext>, IProductDal
    {
        public ProductDto GetByIdWithCategory(int id)
        {
            using (var context = new UcsanContext())
            {
                var result = from p in context.Product
                             join c in context.Category on p.CategoryId equals c.Id
                             select new ProductDto
                             {
                                 Id = p.Id,
                                 CategoryId = p.CategoryId,
                                 Code = p.Code,
                                 Name = p.Name,
                                 Description = p.Description,
                                 ImageUrl = p.ImageUrl,
                                 IsActive = p.IsActive,
                                 CategoryName = c.Name,
                             };

                return result.FirstOrDefault(p=>p.Id == id);

            }
        }

        public List<ProductDto> GetListWithCategory()
        {
            using (var context = new UcsanContext())
            {
                var result = from p in context.Product
                             join c in context.Category on p.CategoryId equals c.Id
                             select new ProductDto
                             {
                                 Id = p.Id,
                                 CategoryId = p.CategoryId,
                                 Code = p.Code,
                                 Name = p.Name,
                                 Description = p.Description,
                                 ImageUrl = p.ImageUrl,
                                 IsActive = p.IsActive,
                                 CategoryName = c.Name,
                             };

                return result.ToList();

            }


        }

    }
}
