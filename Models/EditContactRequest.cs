using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCBankContacts.Models
{
    public class EditContactRequest
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Address { get; set; }
        public DateTime Birth { get; set; }
        public string Phone { get; set; }
        public byte? ContactImage { get; set; }
    }
}
