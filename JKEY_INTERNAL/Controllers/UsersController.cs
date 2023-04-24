using Azure;
using JKEY_INTERNAL.Models;
using JKEY_INTERNAL.Models.CustomModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.Data.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JKEY_INTERNAL.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly JkeyInternalContext _context;
        private readonly ILogger<UsersController> _logger;
        public UsersController(JkeyInternalContext context ,ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        // GET: api/<UsersController>
        [HttpGet]
        public ServiceResponse GetUsers(int page = 1, int pageSize = 10)
        {
            try
            {

                Log.Logger.Information("djt me may ngu");
                // Lấy danh sách người dùng từ database hoặc service
                List<User> users = _context.Users.ToList();

                // Tính toán thông tin phân trang
                int totalCount = users.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                // Lấy dữ liệu cho trang hiện tại
                var usersForPage = users.Skip((page - 1) * pageSize).Take(pageSize);

                // Định dạng dữ liệu trả về
                var response = new
                {

                    totalCount = totalCount,
                    totalPages = totalPages,
                    currentPage = page,
                    pageSize = pageSize,
                    users = usersForPage
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
        public async Task<ServiceResponse> SearchUsers(string? fullname = "", string? phone = "", string? email = "", string? username = "", int page = 1, int pageSize = 10)
        {
            try
            {
                var users = _context.Users.Where(u => u.FullName.Contains(fullname) && u.Email.Contains(phone) && u.Email.Contains(email) && u.Email.Contains(username)).ToList();
                int totalCount = users.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                // Lấy dữ liệu cho trang hiện tại
                var usersForPage = users.Skip((page - 1) * pageSize).Take(pageSize);

                // Định dạng dữ liệu trả về
                var response = new
                {

                    totalCount = totalCount,
                    totalPages = totalPages,
                    currentPage = page,
                    pageSize = pageSize,
                    users = usersForPage
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

            //new JsonResult(response);
        }

        [HttpGet("{id}")]
        // GET api/<UsersController>/5
        public async Task<ServiceResponse> getByID(Guid id)
        {
            try
            {
                // Lấy thông tin user từ database dựa vào id
                User user = await _context.Users.FindAsync(id);

                // Kiểm tra xem user có tồn tại hay không
                if (user == null)
                {
                    NotFound(); // Trả về mã 404 Not Found nếu user không tồn tại
                }
                return new ServiceResponse
                {
                    Data = user,
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


        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> InsertUsers([FromBody] User user)
        {
            try
            {
                // Tạo một đối tượng mới cho dữ liệu cần chèn
                var newData = new User()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Phone = user.Phone,
                    Gender = user.Gender,
                    Active = user.Active,
                    DateOfBirth = user.DateOfBirth,
                    UserName = user.UserName,
                    NormalizedUserName = user.NormalizedUserName,
                    Email = user.Email,
                    UserCreated = user.UserCreated,
                    UserModified = user.UserModified,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                // Thêm dữ liệu vào cơ sở dữ liệu
                _context.Users.Add(newData);
                await _context.SaveChangesAsync();

                // Trả về đối tượng dữ liệu đã được chèn dưới dạng JSON
                return new JsonResult(newData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ServiceResponse Edit(Guid id, [FromBody] User user)
        {
            try
            {
                var oldData = _context.Users.Find(id);

                if (oldData == null)
                {
                    NotFound();
                }

                oldData.FullName = user.FullName;
                oldData.Phone = user.Phone;
                oldData.Gender = user.Gender;
                oldData.Email = user.Email;
                oldData.UserModified = user.UserModified;
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
                var oldData = _context.Users.Find(id);
                if (oldData == null)
                {
                    NotFound();
                }
                _context.Users.Remove(oldData);
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
