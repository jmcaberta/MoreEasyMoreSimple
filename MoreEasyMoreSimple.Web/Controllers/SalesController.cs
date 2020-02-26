using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreEasyMoreSimple.Data;
using MoreEasyMoreSimple.Entities.Sales;
using MoreEasyMoreSimple.Web.Models.Sales.Sale;

namespace MoreEasyMoreSimple.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly DBContextMoreEasyMoreSimple _context;

        public SalesController(DBContextMoreEasyMoreSimple context)
        {
            _context = context;
        }      

        // GET: api/Sales/List
         [HttpGet("[action]")]
        public async Task<IEnumerable<SalesViewModel>> List()
        {
            var sale = await _context.Sales.Include(a => a.company)
                                                     .Include(a => a.user)
                                                     .OrderByDescending(a => a.saleId)
                                                     .Take(100)
                                                     .ToListAsync();

            return sale.Select(a => new SalesViewModel
            {
                saleId = a.saleId,
                companyId = a.companyId,
                company = a.company.companyname,
                ustId = a.company.ustId,
                adress = a.company.adress,
                city = a.company.city,
                postalcode = a.company.postalcode,
                country = a.company.country,
                phone = a.company.phone,
                email = a.company.email,
                userId = a.userId,
                user = a.user.userName,
                invoice = a.invoice,
                invoicenumber = a.invoicenumber,
                saledate = a.saledate,
                taxes = a.taxes,
                total = a.total,
                status = a.status                
            });
        }

        // GET: api/Sales/SaleMonth12
        [HttpGet("[action]")]
        public async Task<IEnumerable<QueryViewModel>> SaleMonth12()
        {
            var query = await _context.Sales
                                                     .GroupBy(a=> a.saledate.Month)
                                                     .Select(x => new { label = x.Key, value = x.Sum(a => a.total)})                                                     
                                                     .OrderByDescending(x => x.label)
                                                     .Take(12)
                                                     .ToListAsync();

            return query.Select(a => new QueryViewModel
            {
               label = a.label.ToString(),
               value = a.value

            });
        }

        // GET: api/Sales/ListFilter
        [HttpGet("[action]/{text}")]
        public async Task<IEnumerable<SalesViewModel>> ListFilter([FromRoute] string text)
        {
            var sale = await _context.Sales.Include(a => a.company)
                                                     .Include(a => a.user)
                                                     .Where(a => a.invoicenumber.Contains(text))
                                                     .OrderByDescending(a => a.saleId)
                                                     .ToListAsync();

            return sale.Select(a => new SalesViewModel
            {
                saleId = a.saleId,
                companyId = a.companyId,
                company = a.company.companyname,
                ustId = a.company.ustId,
                adress = a.company.adress,
                city = a.company.city,
                postalcode = a.company.postalcode,
                country = a.company.country,
                phone = a.company.phone,
                email = a.company.email,
                userId = a.userId,
                user = a.user.userName,
                invoice = a.invoice,
                invoicenumber = a.invoicenumber,
                saledate = a.saledate,
                taxes = a.taxes,
                total = a.total,
                status = a.status
            });
        }

        // GET: api/Sales/DateQuery/startDate/endDate
        [HttpGet("[action]/{startDate}/{endDate}")]
        public async Task<IEnumerable<SalesViewModel>> DateQuery([FromRoute] DateTime startDate, DateTime endDate)
        {
            var sale = await _context.Sales.Include(a => a.company)
                                                     .Include(a => a.user)
                                                     .OrderByDescending(a => a.saleId)
                                                     .Where(i => i.saledate >= startDate && i.saledate <= endDate)
                                                     .Take(100)
                                                     .ToListAsync();

            return sale.Select(a => new SalesViewModel
            {
                saleId = a.saleId,
                companyId = a.companyId,
                company = a.company.companyname,
                ustId = a.company.ustId,
                adress = a.company.adress,
                city = a.company.city,
                postalcode = a.company.postalcode,
                country = a.company.country,
                phone = a.company.phone,
                email = a.company.email,
                userId = a.userId,
                user = a.user.userName,
                invoice = a.invoice,
                invoicenumber = a.invoicenumber,
                saledate = a.saledate,
                taxes = a.taxes,
                total = a.total,
                status = a.status
            });
        }

        // GET: api/Sales/DetailList
        [HttpGet("[action]/{saleId}")]
        public async Task<IEnumerable<DetailViewModel>> DetailList([FromRoute] int saleId)
        {
            var detail = await _context.Saledetails.Include(a => a.article)
                                                     .Where(s => s.saleId == saleId)
                                                     .ToListAsync();

            return detail.Select(s => new DetailViewModel
            {
                articleId = s.articleId,
                article = s.article.articlename,
                quantity = s.quantity,
                discount = s.discount,
                price = s.price,
                percent = s.percent
            });
        }

        // GET: api/Sales/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var sale = await _context.Sales.ToListAsync();
            return sale.Select(a => new SelectViewModel
            {
                saleId = a.saleId,
                invoicenumber = a.invoicenumber

            });
        }

        // POST: api/Sales/Create
        [HttpPost("[action]")]
        public async Task<ActionResult<Sale>> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Datesale = DateTime.Now;

            Sale sale = new Sale
            {
                companyId = model.companyId,
                userId = model.userId,
                invoice = model.invoice,
                invoicenumber = model.invoicenumber,
                saledate = Datesale,
                taxes = model.taxes,
                total = model.total,
                status = "Verkauft"
            };


            try
            {
                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();

                var saleId = sale.saleId;
                foreach (var det in model.details)
                {
                    Saledetails details = new Saledetails
                    {
                        saleId = saleId,
                        articleId = det.articleId,
                        quantity = det.quantity,
                        discount = det.discount,
                        price = det.price,
                        percent = det.percent
                    };
                    _context.Saledetails.Add(details);
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

            var sale = await _context.Sales.FirstOrDefaultAsync(a => a.saleId == id);

            if (sale == null)
            {
                return NotFound();
            }

            sale.status = "Stoniert";

            try
            {
                await _context.SaveChangesAsync();
                var details = await _context.Saledetails.Include(a => a.article).Where(d => d.saleId == id).ToListAsync();

                //2. Recorremos los detalles

                foreach (var det in details)

                {

                    //Obtenemos el artículo del detalle actual

                    var articles = await _context.Articles.FirstOrDefaultAsync(a => a.articleId == det.article.articleId);

                    //actualizamos el stock

                    articles.stock = det.article.stock + det.quantity;

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

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.saleId == id);
        }
    }
}
