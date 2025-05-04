using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Contact;
using api.Models;

namespace api.Mappers
{
    public static class ContactMappers
    {
        //Contact -> ContactDTO
        public static ContactDTO ToContactDTO(this Contact contact)
        {
            return new ContactDTO
            {
                Id = contact.Id,
                BirthDate = contact.BirthDate,
                CategoryId = contact.CategoryId,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                SubcategoryId = contact.SubcategoryId,
                Phone = contact.Phone
            };
        }
        //contactDTO -> Contact
        public static Contact ToContactCreateDTO(this CreateContactDTO contact)
        {
            return new Contact
            {
                BirthDate = contact.BirthDate,
                CategoryId = contact.CategoryId,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                SubcategoryId = contact.SubcategoryId,
                Phone = contact.Phone
            };
        }
    }
}