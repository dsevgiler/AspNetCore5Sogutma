using Core.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Product : IEntity // Veritabanı nesnesi olduğunu anlamak için. 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }

        public int CategoryId { get; set; }



        //public virtual Category Category { get; set; }



    }
}
