using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCBankContacts.Entities;
using ABCBankContacts.Interfaces;
using Microsoft.AspNetCore.Http;
using ABCBankContacts.Models;
using System.Globalization;
using Microsoft.AspNetCore.Cors;

namespace ABCBankContacts.Controllers
{
    [EnableCors("CorsApi")]
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IContactsRepository _contactsRepository;
        private readonly CultureInfo _dateFormat;
        private readonly IConfiguration _configuration;
        public ContactsController(ApplicationContext context, IConfiguration configuration,IContactsRepository contactsRepository) {
            _context = context;
            _configuration = configuration;
            _contactsRepository = contactsRepository;
            _dateFormat = new CultureInfo(_configuration.GetValue<string>("DateFormat"));
        }

        [HttpGet("all")]
        public IActionResult GetAll() {
            var allContacts = _context.Contacts;
            return Ok(allContacts);
        }

        [HttpPut("edit/{id}")]
        public IActionResult Update(int id, [FromBody] EditContactRequest request) {
            var contact = _context.Contacts.FirstOrDefault(x=>x.UserID==id);

            if(contact == null) {
                return NotFound();
            }
            contact.FirstName = request.FirstName;
            contact.SecondName = request.SecondName;
            //contact.Birth
            contact.Address = request.Address;
            contact.Phone = request.Phone;
            contact.ContactImage = request.ContactImage;

            _context.Update(contact);
            _context.SaveChanges();

            return Ok("Contact Updated");
        }


        [HttpPost("new")]
        public IActionResult Create([FromBody] NewContactRequest request) {

            var contact = new Contact
            {
                //birth
                UserID = request.UserID,
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                Address = request.Address,
                Phone = request.Phone,
                Birth = DateTime.Parse(request.Birth, _dateFormat, DateTimeStyles.AdjustToUniversal),
                ContactImage = request.ContactImage
            };

            _context.Add(contact);
            _context.SaveChanges();
            return Ok("New contact created");
        }

        [HttpDelete("del/{id}")]
        public IActionResult Delete(int id){
            var contact = _context.Contacts.FirstOrDefault(x => x.UserID == id);
            if (contact == null)
            {
                return NotFound("The element to delete wasn't found");
            }
            else
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
                return Ok("Deleted!");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Contact>>> Search(string name, string address){
            try
            {
                var result = await _contactsRepository.Search(name, address);
                if (result.Any()) {
                    return Ok(result);
                }
                return NotFound();
            }
            catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error retrieving data from the database");
            }
        }

        [HttpGet("{age}")]
        public async Task<ActionResult<IEnumerable<Contact>>> Age(int start, int ends) {
            try
            {
                var result = await _contactsRepository.AgeRange(start,ends);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error retrieving data from the database");
            }
        }

    }
}
