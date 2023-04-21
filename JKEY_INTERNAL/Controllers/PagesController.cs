using JKEY_INTERNAL.Models;
using JKEY_INTERNAL.Models.CustomModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JKEY_INTERNAL.Controllers
{
    [Route("api/Page")]
    [ApiController]
    [AllowAnonymous]
    public class PagesController : ControllerBase
    {
        private readonly JkeyInternalContext _context;
        public PagesController(JkeyInternalContext context)
        {
            _context = context;

        }
        // GET: api/<PagesController>
        [HttpGet]
        public ServiceResponse GetPages()
        {
            try
            {
                List<Page> pages = _context.Pages.ToList();

                return new ServiceResponse
                {
                    Data = pages,
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
            // Lấy danh sách người dùng từ database hoặc service

        }

        // GET api/<PagesController>/5


        // POST api/<PagesController>
        [HttpPost]
        public async Task<IActionResult> InsertPages([FromBody] Page page)
        {
            try
            {
                // Tạo một đối tượng mới cho dữ liệu cần chèn
                var newData = new Page()
                {
                    Id = page.Id,
                    Name = page.Name,
                    SourcePath = page.SourcePath,
                    Active = page.Active,
                    BackPageId = page.BackPageId,
                    UserCreated = page.UserCreated,
                    UserModified = page.UserModified,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                // Thêm dữ liệu vào cơ sở dữ liệu
                _context.Pages.Add(newData);
                await _context.SaveChangesAsync();

                // Trả về đối tượng dữ liệu đã được chèn dưới dạng JSON
                return new JsonResult(newData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PagesController>/5
        [HttpPut("{id}")]
        public ServiceResponse Edit(string id, [FromBody] Page page)
        {
            try
            {
                var oldData = _context.Pages.Find(id);

                if (oldData == null)
                {
                    NotFound();
                }

                oldData.Name = page.Name;
                oldData.SourcePath = page.SourcePath;
                oldData.Active = page.Active;
                oldData.BackPageId = page.BackPageId;
                oldData.UserModified = page.UserModified;
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

        // DELETE api/<PagesController>/5
        [HttpDelete("{id}")]
        public ServiceResponse Delete(string id)
        {
            try
            {
                var oldData = _context.Pages.Find(id);
                if (oldData == null)
                {
                    NotFound();
                }
                _context.Pages.Remove(oldData);
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
