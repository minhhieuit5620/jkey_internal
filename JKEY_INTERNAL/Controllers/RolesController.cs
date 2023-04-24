using Azure;
using JKEY_INTERNAL.Models;
using JKEY_INTERNAL.Models.CustomModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JKEY_INTERNAL.Controllers
{
    [Route("api/roles")]
    [ApiController]
    //[Authorize(Policy = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly JkeyInternalContext _context;
        public RolesController(JkeyInternalContext context)
        {
            _context = context;

        }
        // GET: api/<RolesController>
        [HttpGet]
        public ServiceResponse GetRoles(int page = 1, int pageSize = 10)
        {
            try
            {
                // Lấy danh sách người dùng từ database hoặc service
                List<Role> role = _context.Roles.ToList();

                // Tính toán thông tin phân trang
                int totalCount = role.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                // Lấy dữ liệu cho trang hiện tại
                var rolesForPage = role.Skip((page - 1) * pageSize).Take(pageSize);

                // Định dạng dữ liệu trả về
                var response = new
                {

                    totalCount = totalCount,
                    totalPages = totalPages,
                    currentPage = page,
                    pageSize = pageSize,
                    roles = rolesForPage
                };

                // Tạo header phân trang
                var paginationHeader = new PaginationHeader(page, pageSize, totalCount, totalPages);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationHeader));

                return new ServiceResponse
                {
                    Data = response,
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Data = ex
                };
            }

        }


        [HttpGet("search")]
        public async Task<ServiceResponse> SearchRoles(string? rolename = "", bool active = true, int page = 1, int pageSize = 10)
        {
            try
            {
                var roles = _context.Roles.Where(u => u.RoleName.Contains(rolename) && u.Active.Equals(active)).ToList();
                int totalCount = roles.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                // Lấy dữ liệu cho trang hiện tại
                var rolesForPage = roles.Skip((page - 1) * pageSize).Take(pageSize);

                // Định dạng dữ liệu trả về
                var response = new
                {

                    totalCount = totalCount,
                    totalPages = totalPages,
                    currentPage = page,
                    pageSize = pageSize,
                    roles = rolesForPage
                };

                // Tạo header phân trang
                var paginationHeader = new PaginationHeader(page, pageSize, totalCount, totalPages);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationHeader));

                return new ServiceResponse
                {
                    Data = response,
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Data = ex
                };
            }

        }


        // GET api/<UsersController>/5
        public async Task<ServiceResponse> getByID(Guid id)
        {
            try
            {
                // Lấy thông tin user từ database dựa vào id
                Role role = await _context.Roles.FindAsync(id);

                // Kiểm tra xem user có tồn tại hay không
                if (role == null)
                {
                    NotFound(); // Trả về mã 404 Not Found nếu user không tồn tại
                }

                return new ServiceResponse
                {
                    Data = role,
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                }; // Trả về thông tin user nếu user tồn tại
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Data = ex
                };
            }

        }


        // POST api/<RolesController>
        [HttpPost]
        public async Task<IActionResult> InsertRoles([FromBody] Role role)
        {
            try
            {
                // Tạo một đối tượng mới cho dữ liệu cần chèn
                var newData = new Role()
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    Active = role.Active,
                    UserCreated = role.UserCreated,
                    UserModified = role.UserModified,
                    Name = role.Name,
                    NormalizedName = role.NormalizedName,
                    ConcurrencyStamp = role.ConcurrencyStamp,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                // Thêm dữ liệu vào cơ sở dữ liệu
                _context.Roles.Add(newData);
                await _context.SaveChangesAsync();

                // Trả về đối tượng dữ liệu đã được chèn dưới dạng JSON
                return new JsonResult(newData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ServiceResponse Edit(Guid id, [FromBody] Role role)
        {
            try
            {
                var oldData = _context.Roles.Find(id);

                if (oldData == null)
                {
                    NotFound();
                }

                oldData.RoleName = role.RoleName;
                oldData.Active = role.Active;
                oldData.UserCreated = role.UserCreated;
                oldData.UserModified = role.UserModified;
                oldData.Name = role.Name;
                oldData.DateCreated = DateTime.UtcNow;
                oldData.DateModified = DateTime.UtcNow;
                // Update other properties as needed

                _context.SaveChanges();

                return new ServiceResponse
                {
                    Data = oldData,
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Data = ex
                };
            }

        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public ServiceResponse Delete(Guid id)
        {
            try
            {
                var oldData = _context.Roles.Find(id);
                if (oldData == null)
                {
                    NotFound();
                }
                _context.Roles.Remove(oldData);
                _context.SaveChanges();
                return new ServiceResponse
                {
                    Data = oldData,
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = false,
                    Data = ex
                };
            }

        }
    }
}
