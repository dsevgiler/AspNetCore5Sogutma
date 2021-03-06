using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }

        public string CategoryName { get; set; }
    }
}
