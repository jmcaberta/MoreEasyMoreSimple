using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreEasyMoreSimple.Data;
using MoreEasyMoreSimple.Entities.Sales;
using MoreEasyMoreSimple.Web.Models.Sales.Branch;

namespace MoreEasyMoreSimple.Web.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly DBContextMoreEasyMoreSimple _context;

        public BranchesController(DBContextMoreEasyMoreSimple context)
        {
            _context = context;
        }

        // GET: api/Branches/List
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<BranchViewModel>> List()
        {
            var branch = await _context.Branches.ToListAsync();
            return branch.Select(b => new BranchViewModel
            {
                branchId = b.branchId,
                branchname = b.branchname,
                branchdesc = b.branchdesc,
                condition = b.condition
            });
        }


        // GET: api/Companiestatus/Select
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung, Mitarbeiter")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<BranchSelectViewModel>> Select()
        {
            var branch = await _context.Branches.Where(s => s.condition == true).ToListAsync();
            return branch.Select(b => new BranchSelectViewModel
            {
                branchId = b.branchId,
                branchname = b.branchname
            });
        }

        // GET: api/Branches/Show/1
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung, Mitarbeiter")]
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Show([FromRoute]int id)
        {
            var branch = await _context.Branches.FindAsync(id);

            if (branch == null)
            {
                return NotFound();
            }

            return Ok(new BranchViewModel
            {
                branchId = branch.branchId,
                branchname = branch.branchname,
                branchdesc = branch.branchdesc,
                condition = branch.condition
            });
        }

        // PUT: api/Branches/Update 
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] BranchUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.branchId <= 0)
            {
                return BadRequest();
            }

            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.branchId == model.branchId);

            if (branch == null)
            {
                return NotFound();
            }

            branch.branchname = model.branchname;
            branch.branchdesc = model.branchdesc;

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

        // POST: api/Branches/Create
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung")]
        [HttpPost("[action]")]
        public async Task<ActionResult<Branch>> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Branch branch = new Branch
            {
                branchname = model.branchname,
                branchdesc = model.branchdesc,
                condition = true
            };

            _context.Branches.Add(branch);

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

        // DELETE: api/Branches/Remove
        [Authorize(Roles = "Administrator, Clerk")]
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Branch>> Remove(int id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            _context.Branches.Remove(branch);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }


            return Ok(branch);
        }

        // PUT: api/Branches/Deactivate
        //[Authorize(Roles = "Administrator, Clerk")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.branchId == id);

            if (branch == null)
            {
                return NotFound();
            }

            branch.condition = false;

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
        //[Authorize(Roles = "Administrator, Clerk")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.branchId == id);

            if (branch == null)
            {
                return NotFound();
            }

            branch.condition = true;

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

        private bool BranchExists(int id)
        {
            return _context.Branches.Any(e => e.branchId == id);
        }
    }
}
