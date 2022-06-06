using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankContacts;

namespace ABCBankContacts.Models
{
    public class NewContactRequest
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Address { get; set; }
        public string Birth { get; set; }
        public string Phone { get; set; }
        public byte? ContactImage { get; set; }
    }
}
