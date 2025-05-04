using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        
        public int CategoryId { get; set; }
        public  Category Category { get; set; } 
        public int? SubcategoryId { get; set; }
        public Subcategory? subcategory { get; set; }
        

    }
}