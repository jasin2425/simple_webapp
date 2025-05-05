using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Contact;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDBContext _context;
        public ContactRepository(ApplicationDBContext context)
        {
            _context=context;
        }

        public async Task<Contact> CreateAsync(Contact contactModel)
        {
            await _context.Contacts.AddAsync(contactModel);
            await _context.SaveChangesAsync();
            return contactModel;
        }

        public async Task<Contact?> DeleteAsync(int id)
        {
            var contactModel= await _context.Contacts.FirstOrDefaultAsync(x => x.Id ==id);
            if(contactModel==null){
                return null;
            }
            _context.Contacts.Remove(contactModel);
            await _context.SaveChangesAsync();
            return contactModel;
        }

        public async Task<List<Contact>> GetAllasync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<Contact?> UpdateAsync(int id, Contact contactModel)
        {
            var existingContact = await _context.Contacts.FindAsync(id);
            if(existingContact==null)
            {
                return null;
            }
            existingContact.BirthDate=contactModel.BirthDate;
             existingContact.Email=contactModel.Email;
            existingContact.FirstName=contactModel.FirstName;
            existingContact.LastName=contactModel.LastName;
            existingContact.Phone=contactModel.Phone;
            await _context.SaveChangesAsync();
            return existingContact;
        }
    }
}