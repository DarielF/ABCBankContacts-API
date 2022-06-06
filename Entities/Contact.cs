using System;
using System.ComponentModel.DataAnnotations;


namespace ABCBankContacts.Entities
{
    public class Contact
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Address { get; set; }
        public DateTime Birth { get; set; }
        public string Phone { get; set; }
        public byte? ContactImage { get; set; }
    }
}
