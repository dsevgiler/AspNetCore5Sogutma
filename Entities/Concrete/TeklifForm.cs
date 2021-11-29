using Core.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class TeklifForm : IEntity
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gsm { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Sektor { get; set; }
        public string Talebiniz { get; set; }
        public string SuGirisSicakligi { get; set; }
        public string SuCikisSicakligi { get; set; }
        public string SaatekiSuMiktari_m3h { get; set; }
        public string IstediğinizKapasite_kwkcal { get; set; }
        public bool IsActive { get; set; }

    }
}
