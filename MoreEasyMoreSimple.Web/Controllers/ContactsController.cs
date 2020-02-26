using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreEasyMoreSimple.Data;
using MoreEasyMoreSimple.Entities.Sales;
using MoreEasyMoreSimple.Web.Models.Sales.Contact;

namespace MoreEasyMoreSimple.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly DBContextMoreEasyMoreSimple _context;

        public ContactsController(DBContextMoreEasyMoreSimple context)
        {
            _context = context;
        }

        // GET: api/Companies/List
        [HttpGet("[action]")]
        public async Task<IEnumerable<ContactViewModel>> List()
        {
            var contacts = await _context.Contacts.Include(c => c.company).ToListAsync();
            return contacts.Select(c => new ContactViewModel
            {
                contactId = c.contactId,
                companyId = c.companyId,
                company = c.company.companyname,
                title = c.title,
                contactname = c.contactname,
                lastname = c.lastname,
                email = c.email,
                phone = c.phone,
                comment = c.comment,               
                condition = c.condition
            });
        }

        // GET: api/Contacts/Show/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Show([FromRoute]int id)
        {
            var contacts = await _context.Contacts.FindAsync(id);

            if (contacts == null)
            {
                return NotFound();
            }

            return Ok(new ContactViewModel
            {
                contactId = contacts.contactId,
                companyId = contacts.companyId,
                company = contacts.company.companyname,
                title = contacts.title,
                contactname = contacts.contactname,
                lastname = contacts.lastname,
                email = contacts.email,
                phone = contacts.phone,
                comment = contacts.comment,
                condition = contacts.condition
            });
        }

        // POST: api/Contacts/Update             
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.contactId <= 0)
            {
                return BadRequest();
            }

            var contacts = await _context.Contacts.FirstOrDefaultAsync(c => c.contactId == model.contactId);

            if (contacts == null)
            {
                return NotFound();
            }

            contacts.companyId = model.companyId;
            contacts.title = model.title;
            contacts.contactname = model.contactname;
            contacts.lastname = model.lastname;
            contacts.email = model.email;
            contacts.phone = model.phone;
            contacts.comment = model.comment;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Contacts/Create      
        [HttpPost("[action]")]
        public async Task<ActionResult<Contact>> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contact contacts = new Contact
            {
                companyId = model.companyId,
                title = model.title,
                contactname = model.contactname,
                lastname = model.lastname,
                email = model.email,
                phone = model.phone,
                comment = model.comment,                
                condition = true
            };

            _context.Contacts.Add(contacts);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }


            return Ok();
        }

        // DELETE: api/Contacts/Remove
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Contact>> Remove(int id)
        {
            var contacts = await _context.Contacts.FindAsync(id);
            if (contacts == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contacts);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }


            return Ok(contacts);
        }

        // PUT: api/Contacts/Deactivate       
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var contacts = await _context.Contacts.FirstOrDefaultAsync(c => c.contactId == id);

            if (contacts == null)
            {
                return NotFound();
            }

            contacts.condition = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Save the exeption
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Contacts/Activate
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var contacts = await _context.Contacts.FirstOrDefaultAsync(c => c.contactId == id);

            if (contacts == null)
            {
                return NotFound();
            }

            contacts.condition = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Save the exeption
                return BadRequest();
            }

            return Ok();
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.contactId == id);
        }
    }
}
