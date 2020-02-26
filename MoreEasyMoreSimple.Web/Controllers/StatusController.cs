using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreEasyMoreSimple.Data;
using MoreEasyMoreSimple.Entities.Sales;
using MoreEasyMoreSimple.Web.Models.Sales.Companystatus;

namespace MoreEasyMoreSimple.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly DBContextMoreEasyMoreSimple _context;

        public StatusController(DBContextMoreEasyMoreSimple context)
        {
            _context = context;
        }

        // GET: api/Companiestatus/List
       
        [HttpGet("[action]")]
        public async Task<IEnumerable<CompanystatusViewModel>> List()
        {
            var companystatus = await _context.Status.ToListAsync();
            return companystatus.Select(s => new CompanystatusViewModel
            {
                statusId = s.statusId,
                statusname = s.statusname,                
                condition = s.condition
            });
        }

        // GET: api/Companiestatus/Select

        [HttpGet("[action]")]
        public async Task<IEnumerable<CompanystatusSelectViewModel>> Select()
        {
            var companystatus = await _context.Status.Where(s => s.condition == true).ToListAsync();
            return companystatus.Select(s => new CompanystatusSelectViewModel
            {
                statusId = s.statusId,
                statusname = s.statusname
               
            });
        }

        // GET: api/Categories/Show
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Show([FromRoute]int id)
        {
            var companystatus = await _context.Status.FindAsync(id);

            if (companystatus == null)
            {
                return NotFound();
            }

            return Ok(new CompanystatusViewModel
            {
                statusId = companystatus.statusId,
                statusname = companystatus.statusname,                
                condition = companystatus.condition
            });
        }

        // PUT: api/Companiestatus/Update
               
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] CompanystatusUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.statusId <= 0)
            {
                return BadRequest();
            }

            var companystatus = await _context.Status.FirstOrDefaultAsync(s => s.statusId == model.statusId);

            if (companystatus == null)
            {
                return NotFound();
            }

            companystatus.statusname = model.statusname;            

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

        // POST: api/Companiestatus/Create      
        [HttpPost("[action]")]
        public async Task<ActionResult<Status>> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Status companystatus = new Status
            {
                statusname = model.statusname,               
                condition = true
            };

            _context.Status.Add(companystatus);

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

        // DELETE: api/Companiestatus/5
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Status>> Remove(int id)
        {
            var companystatus = await _context.Status.FindAsync(id);
            if (companystatus == null)
            {
                return NotFound();
            }

            _context.Status.Remove(companystatus);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }


            return Ok(companystatus);
        }

        // PUT: api/Companiestatus/Deactivate       
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var companystatus = await _context.Status.FirstOrDefaultAsync(s => s.statusId == id);

            if (companystatus == null)
            {
                return NotFound();
            }

            companystatus.condition = false;

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
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var companystatus = await _context.Status.FirstOrDefaultAsync(s => s.statusId == id);

            if (companystatus == null)
            {
                return NotFound();
            }

            companystatus.condition = true;

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

        private bool CompanystatusExists(int id)
        {
            return _context.Status.Any(e => e.statusId == id);
        }
    }
}
