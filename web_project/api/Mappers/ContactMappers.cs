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
        public static ContactDetailDTO toDetailContactDto(this Contact contact)
        {
            return new ContactDetailDTO{
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                BirthDate = contact.BirthDate,
                Email = contact.Email,
                Phone = contact.Phone,
                CategoryName=contact.Category?.Name??"Empty Category",
                 SubcategoryName = contact.subcategory?.Name

                
            };
        }

        //contact -> minimize contact
        public static ContactMinimizeDTO toMinimalisticContactDto(this Contact contact)
        {
            return new ContactMinimizeDTO
            {
                Id=contact.Id,
                FirstName=contact.FirstName,
                LastName=contact.LastName
            };
        }
        //contactDTO -> Contact
        public static Contact ToContactCreateDTO(this CreateContactDTO contact,int categoryId)
        {
            return new Contact
            {
                BirthDate = contact.BirthDate,
                CategoryId = categoryId,
               SubcategoryId=contact.SubcategoryId,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Phone = contact.Phone,
                
            };
        }
        //updateContactDto -> contact
         public static Contact ToContactUpdateDTO(this UpdateContactDTO contact)
        {
            return new Contact
            {
                BirthDate = contact.BirthDate,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Phone = contact.Phone,
                CategoryId=contact.CategoryId,
                SubcategoryId=contact.CategoryId
            };
        }
    }
}