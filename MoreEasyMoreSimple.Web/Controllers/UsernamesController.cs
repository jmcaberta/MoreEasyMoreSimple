using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoreEasyMoreSimple.Data;
using MoreEasyMoreSimple.Entities.Users;
using MoreEasyMoreSimple.Web.Models.Users.Username;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace MoreEasyMoreSimple.Web.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class UsernamesController : ControllerBase
    {
        private readonly DBContextMoreEasyMoreSimple _context;
        private readonly IConfiguration _config;

        public UsernamesController(DBContextMoreEasyMoreSimple context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Usernames/List
        [Authorize(Roles = "Administrator, Clerk")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsernameViewModel>> List()
        {
            var usernames = await _context.Usernames.Include(u => u.rol).ToListAsync();
            return usernames.Select(u => new UsernameViewModel
            {
                userId = u.userId,
                rolId = u.rolId,
                rol = u.rol.rolname,
                userName = u.userName,
                email = u.email,
                telephone = u.telephone,
                password_hash = u.password_hash,
                condition = u.condition
            });
        }

        // POST: api/Usernames/Create 
        //[Authorize(Roles = "Administrator")]
        [HttpPost("[action]")]
        public async Task<ActionResult> Create([FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.email.ToLower();

            if(await _context.Usernames.AnyAsync(u => u.email == email))
            {
                return BadRequest("Das Email wurde schon anwendet");
            }

            CreatePasswordHash(model.password,out byte[] passwordHash, out byte[] passwordSalt);

            Username usernames = new Username
            {
                rolId = model.rolId,
                userName = model.userName,
                email = model.email.ToLower(),
                telephone = model.telephone,
                password_hash = passwordHash,
                password_salt = passwordSalt,
                condition = true
            };

            _context.Usernames.Add(usernames);

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

        // PUT: api/Usernames/Update 
        //[Authorize(Roles = "Administrator")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.userId <= 0)
            {
                return BadRequest();
            }

            var usernames = await _context.Usernames.FirstOrDefaultAsync(u => u.userId == model.userId);

            if (usernames == null)
            {
                return NotFound();
            }

            usernames.rolId = model.rolId;
            usernames.userName = model.userName;
            usernames.email = model.email.ToLower();
            usernames.telephone = model.telephone;
               
            if(model.act_password == true)
            {
                CreatePasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
                usernames.password_hash = passwordHash;
                usernames.password_salt = passwordSalt;
            }

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

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using ( var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        // DELETE: api/Usernames/Remove
        [Authorize(Roles = "Administrator")]
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Username>> Remove(int id)
        {
            var usernames = await _context.Usernames.FindAsync(id);
            if (usernames == null)
            {
                return NotFound();
            }

            _context.Usernames.Remove(usernames);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }


            return Ok(usernames);
        }

        // PUT: api/Usernames/Deactivate       
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Deactivate([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usernames = await _context.Usernames.FirstOrDefaultAsync(u => u.userId == id);

            if (usernames == null)
            {
                return NotFound();
            }

            usernames.condition = false;

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

            var usernames = await _context.Usernames.FirstOrDefaultAsync(u => u.userId == id);

            if (usernames == null)
            {
                return NotFound();
            }

            usernames.condition = true;

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

        
        [HttpPost("[action]")]
        public async Task<IActionResult> Login (LoginViewModel model)
        {
            var email = model.email.ToLower();

            var user = await _context.Usernames.Where(u => u.condition == true).Include(u => u.rol).FirstOrDefaultAsync(u => u.email == email);

            if(user == null)
            {
                return NotFound();
            }
            if(!PasswordHashVerificy(model.password, user.password_hash, user.password_salt))
            {
                return NotFound();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, user.rol.rolname),
                new Claim("userId", user.userId.ToString()),
                new Claim("rol", user.rol.rolname),
                new Claim("userName", user.userName)
            };

            return Ok(
                    new { token = GenerateToken(claims) }
                );
        }

        private bool PasswordHashVerificy(string password, byte[] passwordHashStored, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHashNew = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashStored).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNew));
            }
        }

        private string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds,
              claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //private string GenerateToken(List<Claim> claims)
        // {
        //     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        //     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //     var token = new JwtSecurityToken(
        //         _config["Jwt:Issuer"],
        //         _config["Jwt:Issuer"],
        //         expires: DateTime.Now.AddMinutes(60),
        //         signingCredentials: creds,
        //         claims: claims);

        //     return new JwtSecurityTokenHandler().WriteToken(token);
        // }


        private bool UsernameExists(int id)
        {
            return _context.Usernames.Any(e => e.userId == id);
        }
    }
}
