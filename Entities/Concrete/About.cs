using Core.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class About: IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Misyon { get; set; }
        public string Vizyon { get; set; }
        public string MusteriOdaklilik { get; set; }
        public string DurustlukProfesyonellik { get; set; }
        public string Ilkeler { get; set; }
        public bool IsActive { get; set; }

    }
}
