using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreEasyMoreSimple.Data;
using MoreEasyMoreSimple.Entities.Warehouse;
using MoreEasyMoreSimple.Web.Models.Warehouse.Category;

namespace MoreEasyMoreSimple.Web.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DBContextMoreEasyMoreSimple _context;

        public CategoriesController(DBContextMoreEasyMoreSimple context)
        {
            _context = context;
        }

        // GET: api/Categories/List
        [Authorize(Roles = "Administrator, Clerk, Lagerverwaltung")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoryViewModel>> List()
        {
            var category = await _context.Categories.ToListAsync();
            return category.Select(c => new CategoryViewModel
            {
                categoryId = c.categoryId,
                categoryname = c.categoryname,
                categorydesc = c.categorydesc,
                condition = c.condition
            });
        }

        // GET: api/Categories/Show/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Show([FromRoute]int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(new CategoryViewModel
            {
                categoryId = category.categoryId,
                categoryname = category.categoryname,
                categorydesc = category.categorydesc,
                condition = category.condition
            });
        }

        // GET: api/Categories/Select
        [Authorize(Roles = "Administrator, Clerk, Lagerverwaltung, Mitarbeiter")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var category = await _context.Categories.Where(c => c.condition == true).ToListAsync();
            return category.Select(c => new SelectViewModel
            {
                categoryId = c.categoryId,
                categoryname = c.categoryname

            });
        }

        // PUT: api/Categories/Update 
        //[Authorize(Roles = "Administrator, Clerk")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.categoryId <= 0)
            {
                return BadRequest();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.categoryId == model.categoryId);

            if (category == null)
            {
                return NotFound();
            }

            category.categoryname = model.categoryname;
            category.categorydesc = model.categorydesc;

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

        // POST: api/Categories/Create
        //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung")]
        [HttpPost("[action]")]
        public async Task<ActionResult<Category>> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Category category = new Category
            {
                categoryname = model.categoryname,
                categorydesc = model.categorydesc,
                condition = true
            };

            _context.Categories.Add(category);

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
        //[Authorize(Roles = "Administrator, Clerk")]
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Category>> Remove(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
            

            return Ok(category);
        }

        // PUT: api/Categories/Deactivate
        //[Authorize(Roles = "Administrator, Clerk")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {
            
            if (id <= 0)
            {
                return BadRequest();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.categoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            category.condition = false;

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

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.categoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            category.condition = true;

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

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.categoryId == id);
        }
    }
}
