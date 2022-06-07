using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSharpRestAPI;

namespace CSharpRestAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TestDBContext _context;

        public UserController(TestDBContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("getuser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("adduser/")]
        public async Task<ActionResult<String>> PutUser(User user)
        {
            if (UserExists(user.UserName))
            {
                return "Duplicated UserName!!!";
            }
            else
            {
                return "Add failed!!!";
            }
            try
            {
                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return "Add successfull!!!";
        }

        // POST: api/User
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("updateuser/")]
        public async Task<ActionResult<String>> PostUser(User user)
        {
            if(!IdExists(user.Id))
            {
                return "Can not find User with ID: "+ user.Id + "!!!";
            }
            else
            {
                try
                {
                    _context.User.Update(user);
                    await _context.SaveChangesAsync();
                    return "Update successfull!!!";
                }
                catch
                {
                    return "Update failed!!!";
                }
            }
            
            
        }

        // DELETE: api/User/5
        [HttpDelete("deleteuser/{id}")]
        public async Task<ActionResult<String>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return "Can not find user with ID: "+ id+"!!!";
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return "User delete successfull!!!";
        }

        private bool UserExists(string UserName)
        {
            return _context.User.Any(e => e.UserName == UserName);
        }
        private bool IdExists(int Id)
        {
            return _context.User.Any(e => e.Id == Id);
        }
    }
}
