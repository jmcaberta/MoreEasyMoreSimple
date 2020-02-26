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
using MoreEasyMoreSimple.Web.Models.Sales.Company;

namespace MoreEasyMoreSimple.Web.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly DBContextMoreEasyMoreSimple _context;

        public CompaniesController(DBContextMoreEasyMoreSimple context)
        {
            _context = context;
        }

        // GET: api/Companies/List
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<CompanyViewModel>> List()
        {
            var companies = await _context.Companies.Include(c => c.branch)
                                                    .Include(c => c.status).ToListAsync();
            return companies.Select(c => new CompanyViewModel
            {
                companyId = c.companyId,
                statusId = c.statusId,
                status = c.status.statusname,
                branchId = c.branchId,
                branch = c.branch.branchname,
                ustId = c.ustId,
                companyname = c.companyname,
                adress = c.adress,
                city = c.city,
                postalcode = c.postalcode,
                country = c.country,
                phone = c.phone,
                email = c.email,
                condition = c.condition
            });
        }

        // GET: api/Companies/ListClients
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<CompanyViewModel>> ListClients()
        {
            var companies = await _context.Companies.Include(c => c.branch)
                                                    .Include(c => c.status)
                                                    .Where(c => c.statusId == 1)
                                                    .ToListAsync();
            return companies.Select(c => new CompanyViewModel
            {
                companyId = c.companyId,
                statusId = c.statusId,
                status = c.status.statusname,
                branchId = c.branchId,
                branch = c.branch.branchname,
                ustId = c.ustId,
                companyname = c.companyname,
                adress = c.adress,
                city = c.city,
                postalcode = c.postalcode,
                country = c.country,
                phone = c.phone,
                email = c.email,
                condition = c.condition
            });
        }

        // GET: api/Companies/List
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<CompanyViewModel>> ListProvider()
        {
            var companies = await _context.Companies.Include(c => c.branch)
                                                    .Include(c => c.status)
                                                    .Where(c => c.statusId == 2)
                                                    .ToListAsync();
            return companies.Select(c => new CompanyViewModel
            {
                companyId = c.companyId,
                statusId = c.statusId,
                status = c.status.statusname,
                branchId = c.branchId,
                branch = c.branch.branchname,
                ustId = c.ustId,
                companyname = c.companyname,
                adress = c.adress,
                city = c.city,
                postalcode = c.postalcode,
                country = c.country,
                phone = c.phone,
                email = c.email,
                condition = c.condition
            });
        }

        // GET: api/Companies/Show/1
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung, Mitarbeiter")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Show([FromRoute]int id)
        {
            var companies = await _context.Companies.FindAsync(id);

            if (companies == null)
            {
                return NotFound();
            }

            return Ok(new CompanyViewModel
            {
                companyId = companies.companyId,
                statusId = companies.statusId,
                status = companies.status.statusname,
                branchId = companies.branchId,
                branch = companies.branch.branchname,
                ustId = companies.ustId,
                companyname = companies.companyname,
                adress = companies.adress,
                city = companies.city,
                postalcode = companies.postalcode,
                country = companies.country,
                phone = companies.phone,
                email = companies.email,
                condition = companies.condition
            });
        }

        // GET: api/Companies/Select
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung, Mitarbeiter")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var companies = await _context.Companies.Where(c => c.condition == true).ToListAsync();
            return companies.Select(c => new SelectViewModel
            {
                companyId = c.companyId,
                companyname = c.companyname

            });
        }

        // GET: api/Companies/Select
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung, Mitarbeiter")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> SelectProvider()
        {
            var companies = await _context.Companies.Where(c => c.statusId == 2).ToListAsync();
            return companies.Select(c => new SelectViewModel
            {
                companyId = c.companyId,
                companyname = c.companyname

            });
        }

        // GET: api/Companies/Select
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung, Mitarbeiter")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> SelectClient()
        {
            var companies = await _context.Companies.Where(c => c.statusId == 1).ToListAsync();
            return companies.Select(c => new SelectViewModel
            {
                companyId = c.companyId,
                companyname = c.companyname

            });
        }

        // POST: api/Companies/Update
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.companyId <= 0)
            {
                return BadRequest();
            }

            var companies = await _context.Companies.FirstOrDefaultAsync(c => c.companyId == model.companyId);

            if (companies == null)
            {
                return NotFound();
            }

            companies.statusId = model.statusId;
            companies.branchId = model.branchId;
            companies.ustId = model.ustId;
            companies.companyname = model.companyname;
            companies.adress = model.adress;
            companies.city = model.city;
            companies.postalcode = model.postalcode;
            companies.country = model.country;
            companies.phone = model.phone;
            companies.email = model.email;

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

        // POST: api/Companies/Create 
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung")]
        [HttpPost("[action]")]
        public async Task<ActionResult<Company>> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Company companies = new Company
            {
            statusId = model.statusId,
            branchId = model.branchId,
            ustId = model.ustId,
            companyname = model.companyname,
            adress = model.adress,
            city = model.city,
            postalcode = model.postalcode,
            country = model.country,
            phone = model.phone,
            email = model.email,
            condition = true
            };

            _context.Companies.Add(companies);

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

        // DELETE: api/Companies/Remove
        //[Authorize(Roles = "Administrator, Clerk")]
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Company>> Remove(int id)
        {
            var companies = await _context.Companies.FindAsync(id);
            if (companies == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(companies);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }


            return Ok(companies);
        }

        // PUT: api/Companies/Deactivate
        //[Authorize(Roles = "Administrator, Clerk")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var companies = await _context.Companies.FirstOrDefaultAsync(c => c.companyId == id);

            if (companies == null)
            {
                return NotFound();
            }

            companies.condition = false;

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

            var companies = await _context.Companies.FirstOrDefaultAsync(c => c.companyId == id);

            if (companies == null)
            {
                return NotFound();
            }

            companies.condition = true;

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



        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.companyId == id);
        }
    }
}
