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
using MoreEasyMoreSimple.Web.Models.Warehouse.Admission;

namespace MoreEasyMoreSimple.Web.Controllers
{
    //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung, Mitarbeiter")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionsController : ControllerBase
    {
        private readonly DBContextMoreEasyMoreSimple _context;

        public AdmissionsController(DBContextMoreEasyMoreSimple context)
        {
            _context = context;
        }

        // GET: api/Admissions/List
        [HttpGet("[action]")]
        public async Task<IEnumerable<AdmissionViewModel>> List()
        {
            var admission = await _context.Admissions.Include(a => a.company)
                                                     .Include(a => a.user)
                                                     .OrderByDescending(a => a.admissionId)
                                                     .Take(100)
                                                     .ToListAsync();

            return admission.Select(a => new AdmissionViewModel
            {
                admissionId = a.admissionId,
                companyId = a.companyId,
                company = a.company.companyname,
                userId = a.userId,
                user = a.user.userName,
                invoice = a.invoice,
                invoicenumber = a.invoicenumber,
                admissiondate = a.admissiondate,
                taxes = a.taxes,
                total = a.total,
                status = a.status                
            });
        }


        // GET: api/Admissions/ListFilter
        [HttpGet("[action]/{text}")]
        public async Task<IEnumerable<AdmissionViewModel>> ListFilter([FromRoute] string text)
        {
            var admission = await _context.Admissions.Include(a => a.company)
                                                     .Include(a => a.user)
                                                     .Where(a => a.invoicenumber.Contains(text))
                                                     .OrderByDescending(a => a.admissionId)                                                     
                                                     .ToListAsync();

            return admission.Select(a => new AdmissionViewModel
            {
                admissionId = a.admissionId,
                companyId = a.companyId,
                company = a.company.companyname,
                userId = a.userId,
                user = a.user.userName,
                invoice = a.invoice,
                invoicenumber = a.invoicenumber,
                admissiondate = a.admissiondate,
                taxes = a.taxes,
                total = a.total,
                status = a.status
            });
        }

        // GET: api/Admissions/AdmissionQuery/startDate/endDate
        [HttpGet("[action]/{startDate}/{endDate}")]
        public async Task<IEnumerable<AdmissionViewModel>> AdmissionQuery([FromRoute] DateTime startDate, DateTime endDate)
        {
            var admission = await _context.Admissions.Include(a => a.company)
                                                     .Include(a => a.user)
                                                     .OrderByDescending(a => a.admissionId)
                                                     .Where(i => i.admissiondate >= startDate && i.admissiondate <= endDate)
                                                     .Take(100)
                                                     .ToListAsync();

            return admission.Select(a => new AdmissionViewModel
            {
                admissionId = a.admissionId,
                companyId = a.companyId,
                company = a.company.companyname,
                userId = a.userId,
                user = a.user.userName,
                invoice = a.invoice,
                invoicenumber = a.invoicenumber,
                admissiondate = a.admissiondate,
                taxes = a.taxes,
                total = a.total,
                status = a.status
            });
        }

        // GET: api/Admissions/DetailList
        [HttpGet("[action]/{admissionId}")]
        public async Task<IEnumerable<DetailViewModel>> DetailList([FromRoute] int admissionId)
        {
            var detail = await _context.Admissiondetails.Include(a => a.article)
                                                     .Where(d => d.admissionId == admissionId)
                                                     .ToListAsync();

            return detail.Select(d => new DetailViewModel
            {
                articleId = d.articleId,
                article = d.article.articlename, 
                quantity = d.quantity,
                price = d.price
            });
        }

        // GET: api/Admissions/Show/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Show([FromRoute]int id)
        {
            var admission = await _context.Admissions.FindAsync(id);

            if (admission == null)
            {
                return NotFound();
            }

            return Ok(new AdmissionViewModel
            {
                admissionId = admission.admissionId,
                companyId = admission.companyId,
                company = admission.company.companyname,
                userId = admission.userId,
                user = admission.user.userName,
                invoice = admission.invoice,
                invoicenumber = admission.invoicenumber,
                admissiondate = admission.admissiondate,
                taxes = admission.taxes,
                total = admission.total,
                status = admission.status                
            });
        }

        // GET: api/Admissions/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var admission = await _context.Admissions.ToListAsync();
            return admission.Select(a => new SelectViewModel
            {
                admissionId = a.admissionId,
                invoicenumber = a.invoicenumber

            });
        }

        // POST: api/Admissions/Update             
        //[HttpPut("[action]")]
        //public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (model.admissionId <= 0)
        //    {
        //        return BadRequest();
        //    }

        //    var admission = await _context.Admissions.FirstOrDefaultAsync(a => a.admissionId == model.admissionId);

        //    if (admission == null)
        //    {
        //        return NotFound();
        //    }

        //    admission.admissionId = model.admissionId;
        //    admission.companyId = model.companyId;
        //    admission.userId = model.userId;
        //    admission.invoice = model.invoice;
        //    admission.invoicenumber = model.invoicenumber;
        //    admission.admissiondate = model.admissiondate;
        //    admission.taxes = model.taxes;
        //    admission.total = model.total;
        //    admission.status = model.status;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok();
        //}

        // POST: api/Admissions/Create      

        [HttpPost("[action]")]
        public async Task<ActionResult<Admission>> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Dateadmission = DateTime.Now;

            Admission admission = new Admission
            {

                companyId = model.companyId,
                userId = model.userId,
                invoice = model.invoice,
                invoicenumber = model.invoicenumber,
                admissiondate = Dateadmission,
                taxes = model.taxes,
                total = model.total,
                status = "Aufgenommen"
            };
                       

            try
            {
                _context.Admissions.Add(admission);
                await _context.SaveChangesAsync();

                var admissionId = admission.admissionId;
                foreach(var det in model.details)
                {
                    Admissiondetails details = new Admissiondetails
                    {
                        admissionId = admissionId,
                        articleId = det.articleId,
                        quantity = det.quantity,
                        price = det.price
                    };
                    _context.Admissiondetails.Add(details);
                }

                await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {

                return BadRequest();
            }


            return Ok();
        }

        // PUT: api/Admissions/Override       
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Override([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var admission = await _context.Admissions.FirstOrDefaultAsync(a => a.admissionId == id);

            if (admission == null)
            {
                return NotFound();
            }

            admission.status = "Stoniert";

            try
            {
                await _context.SaveChangesAsync();
                var details = await _context.Admissiondetails.Include(a => a.article).Where(d => d.admissionId == id).ToListAsync();

                //2. Recorremos los detalles

                foreach (var det in details)

                {

                    //Obtenemos el artículo del detalle actual

                    var articles = await _context.Articles.FirstOrDefaultAsync(a => a.articleId == det.article.articleId);

                    //actualizamos el stock

                    articles.stock = det.article.stock - det.quantity;

                    //Guardamos los cambios

                    await _context.SaveChangesAsync();

                }
            }
            catch (DbUpdateConcurrencyException)
            {
                // Save the exeption
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Admissions/Remove
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Admission>> Remove(int id)
        {
            var admission = await _context.Admissions.FindAsync(id);
            if (admission == null)
            {
                return NotFound();
            }

            _context.Admissions.Remove(admission);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }


            return Ok(admission);
        }

        private bool EntranceExists(int id)
        {
            return _context.Admissions.Any(e => e.admissionId == id);
        }
    }
}