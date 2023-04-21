using JKEY_INTERNAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JKEY_INTERNAL.Controllers
{
    [Route("api/userrole")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly JkeyInternalContext _context;
        public UserRolesController(JkeyInternalContext context)
        {
            _context = context;

        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetUserRoleIds(Guid userId)
        {
            var roleIds = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .ToListAsync();

            if (!roleIds.Any())
            {
                return NotFound();
            }

            return Ok(roleIds);
        }

    }
}
