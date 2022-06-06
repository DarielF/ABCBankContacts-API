using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankContacts;
using ABCBankContacts.Entities;
using ABCBankContacts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ABCBankContacts.Services
{

    public class ContactsRepository : IContactsRepository
    {
        private readonly ApplicationContext _context;
        public ContactsRepository(ApplicationContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> Search(string name, string? address) {
            IQueryable<Contact> query = _context.Contacts;

            if (!string.IsNullOrEmpty(name)) {
                query = query.Where(x => x.FirstName.Contains(name) || x.SecondName.Contains(name));
            }

            if (!string.IsNullOrEmpty(name)) {
                query = query.Where(x => x.Address.Contains(address));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Contact>> AgeRange(int? start, int? ends)
        {
            IQueryable<Contact> query = _context.Contacts;

            query = query.Where(x=> Age(x.Birth) >= start && Age(x.Birth) <= ends);

            return await query.ToListAsync();
        }

        private int Age(DateTime dateTime) {
            int year = dateTime.Year;
            int currentYear = DateTime.Today.Year;
            
            return (currentYear - year);

        }
    }
}
