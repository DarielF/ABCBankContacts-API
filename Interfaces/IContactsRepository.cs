using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankContacts.Entities;

namespace ABCBankContacts.Interfaces
{
    public interface IContactsRepository
    {
        Task<IEnumerable<Contact>> Search(string name, string address);
        Task<IEnumerable<Contact>> AgeRange(int? start, int? ends);
    }
}
