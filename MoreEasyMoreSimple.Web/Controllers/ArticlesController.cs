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
using MoreEasyMoreSimple.Web.Models.Warehouse.Article;

namespace MoreEasyMoreSimple.Web.Controllers
{
    //[Authorize(Roles = "Administrator, Clerk, Lagerverwaltung, Mitarbeiter")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly DBContextMoreEasyMoreSimple _context;

        public ArticlesController(DBContextMoreEasyMoreSimple context)
        {
            _context = context;
        }

        // GET: api/Articles/List
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticleViewModel>> List()
        {
            var articles = await _context.Articles.Include(a => a.category)
                                                 .ToListAsync();
            return articles.Select(a => new ArticleViewModel
            {
                articleId = a.articleId,
                categoryId = a.categoryId,
                category = a.category.categoryname,               
                codearticle = a.codearticle,
                articlename = a.articlename,
                sellprice = a.sellprice,
                stock = a.stock,
                articledesc = a.articledesc,                
                condition = a.condition
            });
        }

        // GET: api/Articles/ListAdmission
        [HttpGet("[action]/{text}")]
        public async Task<IEnumerable<ArticleViewModel>> ListAdmission([FromRoute]string text)
        {
            var articles = await _context.Articles.Include(a => a.category)
                                                  //.Include(a => a.company)
                                                  .Where(a => a.articlename.Contains(text))
                                                  .Where(a => a.condition == true).ToListAsync();
            return articles.Select(a => new ArticleViewModel
            {
                articleId = a.articleId,
                categoryId = a.categoryId,
                category = a.category.categoryname,               
                codearticle = a.codearticle,
                articlename = a.articlename,
                sellprice = a.sellprice,
                stock = a.stock,
                articledesc = a.articledesc,
                condition = a.condition
            });
        }

        // GET: api/Articles/ListSale
        [HttpGet("[action]/{text}")]
        public async Task<IEnumerable<ArticleViewModel>> ListSale([FromRoute]string text)
        {
            var articles = await _context.Articles.Include(a => a.category)
                                                  .Where(a => a.stock > 0)
                                                  .Where(a => a.articlename.Contains(text))
                                                  .Where(a => a.condition == true).ToListAsync();
            return articles.Select(a => new ArticleViewModel
            {
                articleId = a.articleId,
                categoryId = a.categoryId,
                category = a.category.categoryname,
                codearticle = a.codearticle,
                articlename = a.articlename,
                sellprice = a.sellprice,
                stock = a.stock,
                articledesc = a.articledesc,
                condition = a.condition
            });
        }

        // GET: api/Articles/Show/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Show([FromRoute]int id)
        {
            var articles = await _context.Articles.FindAsync(id);

            if (articles == null)
            {
                return NotFound();
            }

            return Ok(new ArticleViewModel
            {
                articleId = articles.articleId,
                categoryId = articles.categoryId,
                category = articles.category.categoryname,               
                codearticle = articles.codearticle,
                articlename = articles.articlename,
                sellprice = articles.sellprice,
                stock = articles.stock,
                articledesc = articles.articledesc,
                condition = articles.condition
            });
        }

        // GET: api/Articles/SearchCode/
        [HttpGet("[action]/{codearticle}")]
        public async Task<IActionResult> SearchCode([FromRoute]string codearticle)
        {
            var articles = await _context.Articles.Include(a => a.category)                
                .Where(a => a.condition == true)
                .SingleOrDefaultAsync(a => a.codearticle == codearticle);

            if (articles == null)
            {
                return NotFound();
            }

            return Ok(new ArticleViewModel
            {
                articleId = articles.articleId,
                categoryId = articles.categoryId,
                category = articles.category.categoryname,               
                codearticle = articles.codearticle,
                articlename = articles.articlename,
                sellprice = articles.sellprice,
                stock = articles.stock,
                articledesc = articles.articledesc,
                condition = articles.condition
            });
        }

        // GET: api/Articles/SearchCodeSale/
        [HttpGet("[action]/{codearticle}")]
        public async Task<IActionResult> SearchCodeSale([FromRoute]string codearticle)
        {
            var articles = await _context.Articles.Include(a => a.category)
                .Where(a => a.condition == true)
                .Where(a => a.stock > 0)
                .SingleOrDefaultAsync(a => a.codearticle == codearticle);

            if (articles == null)
            {
                return NotFound();
            }

            return Ok(new ArticleViewModel
            {
                articleId = articles.articleId,
                categoryId = articles.categoryId,
                category = articles.category.categoryname,
                codearticle = articles.codearticle,
                articlename = articles.articlename,
                sellprice = articles.sellprice,
                stock = articles.stock,
                articledesc = articles.articledesc,
                condition = articles.condition
            });
        }

        // GET: api/Articles/Select

        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var article = await _context.Articles.Where(a => a.condition == true).ToListAsync();
            return article.Select(a => new SelectViewModel
            {
                articleId = a.articleId,
                articlename = a.articlename

            });
        }

        // POST: api/Articles/Update             
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.articleId <= 0)
            {
                return BadRequest();
            }

            var article = await _context.Articles.FirstOrDefaultAsync(a => a.articleId == model.articleId);

            if (article == null)
            {
                return NotFound();
            }

                article.articleId = model.articleId;
                article.categoryId = model.categoryId;               
                article.codearticle = model.codearticle;
                article.articlename = model.articlename;
                article.sellprice = model.sellprice;
                article.stock = model.stock;
                article.articledesc = model.articledesc;            

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

        // POST: api/Articles/Create      
        [HttpPost("[action]")]
        public async Task<ActionResult<Article>> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Article article = new Article
            {

            categoryId = model.categoryId,           
            codearticle = model.codearticle,
            articlename = model.articlename,
            sellprice = model.sellprice,
            stock = model.stock,
            articledesc = model.articledesc,
            condition = true
            };

            _context.Articles.Add(article);

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

        // DELETE: api/Articles/Remove
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Article>> Remove(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }


            return Ok(article);
        }

        // PUT: api/Articles/Deactivate       
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var article = await _context.Articles.FirstOrDefaultAsync(a => a.articleId == id);

            if (article == null)
            {
                return NotFound();
            }

            article.condition = false;

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

            var article = await _context.Articles.FirstOrDefaultAsync(a => a.articleId == id);

            if (article == null)
            {
                return NotFound();
            }

            article.condition = true;

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

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.articleId == id);
        }
    }
}
