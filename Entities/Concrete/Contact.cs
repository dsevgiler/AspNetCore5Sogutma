using Core.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Contact : IEntity
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Phone4 { get; set; }
        public string Gsm { get; set; }
        public string Gsm2 { get; set; }
        public string Gsm3 { get; set; }
        public string Gsm4 { get; set; }
        public string Mail { get; set; }
        public string Mail1 { get; set; }
        public string Fax { get; set; }
        public bool IsActive { get; set; }

    }
}
