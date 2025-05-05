using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Contact;
using api.Models;

namespace api.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllasync();
        Task<Contact?> GetByIdAsync(int id);
        Task<Contact> CreateAsync(Contact contactModel);
        Task<Contact?> UpdateAsync(int id,  Contact contactModel);
        Task<Contact?> DeleteAsync(int id);
    }
}