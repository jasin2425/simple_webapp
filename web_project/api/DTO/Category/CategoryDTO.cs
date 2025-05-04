using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Contact;

namespace api.DTO
{
    public class CategoryDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public List<ContactDTO> Contacts{ get;  set;}
    }
}