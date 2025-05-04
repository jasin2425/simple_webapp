using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Contact>? Contacts { get; set; } = new List<Contact>();
        public List<CategoryandSubcategory> CategorySubcategories { get; set; }=new List<CategoryandSubcategory>();

    }
}