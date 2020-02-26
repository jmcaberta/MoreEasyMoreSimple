using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreEasyMoreSimple.Data;
using MoreEasyMoreSimple.Entities.Users;
using MoreEasyMoreSimple.Web.Models.Users.Rol;

namespace MoreEasyMoreSimple.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolsController : ControllerBase
    {
        private readonly DBContextMoreEasyMoreSimple _context;

        public RolsController(DBContextMoreEasyMoreSimple context)
        {
            _context = context;
        }

        // GET: api/Rols/List 
        
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolViewModel>> List()
        {
            var rols = await _context.Rols.ToListAsync();
            return rols.Select(r => new RolViewModel
            {
                rolId = r.rolId,
                rolname = r.rolname,
                roldesc = r.roldesc,
                condition = r.condition
            });
        }

        // GET: api/Rols/Select

        [HttpGet("[action]")]
        public async Task<IEnumerable<RolSelectViewModel>> Select()
        {
            var rols = await _context.Rols.Where(s => s.condition == true).ToListAsync();
            return rols.Select(r => new RolSelectViewModel
            {
                rolId = r.rolId,
                rolname = r.rolname
            });
        }

        // GET: api/Rols/Show/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Show([FromRoute]int id)
        {
            var rols = await _context.Rols.FindAsync(id);

            if (rols == null)
            {
                return NotFound();
            }

            return Ok(new RolViewModel
            {
                rolId = rols.rolId,
                rolname = rols.rolname,
                roldesc = rols.roldesc,
                condition = rols.condition
            });
        }

        // PUT: api/Rols/Update       
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.rolId <= 0)
            {
                return BadRequest();
            }

            var rols = await _context.Rols.FirstOrDefaultAsync(r => r.rolId == model.rolId);

            if (rols == null)
            {
                return NotFound();
            }

            rols.rolname = model.rolname;
            rols.roldesc = model.roldesc;

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

        // POST: api/Rols/Create      
        [HttpPost("[action]")]
        public async Task<ActionResult<Rol>> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Rol rol = new Rol
            {
                rolname = model.rolname,
                roldesc = model.roldesc,
                condition = true
            };

            _context.Rols.Add(rol);

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

        // DELETE: api/Categories/5
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Rol>> Remove(int id)
        {
            var rols = await _context.Rols.FindAsync(id);
            if (rols == null)
            {
                return NotFound();
            }

            _context.Rols.Remove(rols);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }


            return Ok(rols);
        }

        // PUT: api/Rols/Deactivate       
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var rols = await _context.Rols.FirstOrDefaultAsync(r => r.rolId == id);

            if (rols == null)
            {
                return NotFound();
            }

            rols.condition = false;

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

            var rols = await _context.Rols.FirstOrDefaultAsync(r => r.rolId == id);

            if (rols == null)
            {
                return NotFound();
            }

            rols.condition = true;

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


        private bool RolExists(int id)
        {
            return _context.Rols.Any(e => e.rolId == id);
        }
    }
}
