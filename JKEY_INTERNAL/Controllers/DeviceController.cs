using JKEY_INTERNAL.Models.CustomModel;
using JKEY_INTERNAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace JKEY_INTERNAL.Controllers
{
    [ApiController]
    [Route("api/DeviceUser")]
    public class DeviceController : BaseController<DeviceUser>
    {
        private readonly ILogger<DeviceController> _logger;
        private readonly JkeyInternalContext _context;


        public DeviceController(ILogger<DeviceController> logger, JkeyInternalContext dbContext)
        {
            _logger = logger;
            _context = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Lấy toàn bộ system config
        /// </summary>
        /// <returns>Danh sách device user theo thời gian tạo</returns>
        /// CreatedBy: HMHieu
        [HttpGet]
        public ServiceResponse GetAllSystem()
        {
            try
            {
                List<DeviceUser> listDeviceUser = _context.DeviceUsers.OrderByDescending(x => x.UserCreated).ToList();

                return new ServiceResponse
                {
                    StatusCode = StatusCodes.Status200OK,

                    Success = true,
                    Data = listDeviceUser
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

        [HttpGet("filter")]
        public ServiceResponse FilterEmployee(
          [FromQuery] string? value = "",// dữ liệu cần tìm kiếm        
          [FromQuery] string? name = "",// dữ liệu cần tìm kiếm        
          [FromQuery] string? type = "",// dữ liệu cần tìm kiếm        
          [FromQuery] int pageIndex = 1,// trang hiện tại
          [FromQuery] int pageSize = 15)//số bản ghi trong 1 trang
        {

            try
            {
                int skip = ((pageIndex - 1) * pageSize);
                var lst = "";// _context.DeviceUsers.Where(x => x.Value.Contains(value) && x.Name.Contains(name) && x.Type.Contains(type)).OrderByDescending(y => y.DateCreated).ToList();

                if (pageSize != 0)
                {
                    var data = lst.Skip(skip).Take(pageSize);
                    int pages = lst.Count() % pageSize >= 1 ? lst.Count() / pageSize + 1 : lst.Count() / pageSize;
                    return new ServiceResponse
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Success = true,
                        Data = new
                        {
                            data = data,
                            page = pages,
                            total = lst.Count()
                        }
                    };
                }
                else
                {
                    return new ServiceResponse
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Success = true,
                        Data = new
                        {
                            data = lst,
                            pages = 0,
                            total = lst.Count()
                        }

                    };
                }


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



        /// <summary>
        /// Lấy ra device user theo Id
        /// </summary>
        /// <param name="ID">id device user</param>
        /// <returns>Bản ghi tương ứng với ID</returns>
        /// Created By: HMHieu
        [HttpGet("{ID}")]
        public ServiceResponse GetDeviceUserByID([FromRoute] Guid ID)
        {

            try
            {
                DeviceUser DeviceUser = _context.DeviceUsers.Where(x => x.Id == ID).FirstOrDefault();

                if (DeviceUser != null)
                {
                    return new ServiceResponse
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Success = true,
                        Data = DeviceUser
                    };
                }
                else
                {
                    return new ServiceResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Success = false,
                        Data = "Không có bản ghi nào được tìm thấy"
                    };

                }

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

        /// <summary>
        /// Insert dữ liệu vào DB
        /// </summary>
        /// <param name="payload">Dữ liệu truyền vào</param>
        /// <returns>ID bản ghi mới được thêm vào</returns>
        /// CreatedBy: HMHieu
        [HttpPost("[action]")]
        public ServiceResponse Insert([FromBody] DeviceUser payload)
        {
            try
            {
                if (payload == null)
                {
                    return new ServiceResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Success = false,
                        Data = "Đầu vào của device user đang rỗng"
                    };
                }
                else
                {
                    var check = ValidateRequestData(payload);
                    if (check.Success == false)
                    {
                        return new ServiceResponse
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Success = false,
                            Data = check.Data
                        };
                    }
                    DeviceUser DeviceUser = new DeviceUser();
                    Guid id = Guid.NewGuid();
                    DeviceUser.Id = id;
                    DeviceUser.CustomerId = payload.CustomerId;
                    DeviceUser.DeviceId = payload.DeviceId;
                    DeviceUser.Status = payload.Status;
                    DeviceUser.AuthenType = payload.AuthenType;
                    DeviceUser.IssueDate = payload.IssueDate;
                    DeviceUser.CreatedDate = DateTime.Now;
                    DeviceUser.ApprovedDate = payload.ApprovedDate;
                    DeviceUser.DeletedDate = payload.DeletedDate;
                    DeviceUser.ActivedDate = payload.ActivedDate;
                    DeviceUser.ActiveCode = payload.ActiveCode;
                    DeviceUser.UserCreated = payload.UserCreated;
                    DeviceUser.UserDeleted = payload.UserDeleted;
                    DeviceUser.UserApproved = payload.UserApproved;

                    _context.DeviceUsers.Add(DeviceUser);
                    _context.SaveChanges();
                    return new ServiceResponse
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Success = true,
                        Data = id
                    };
                }
            }
            catch (Exception ex)
            {

                return new ServiceResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Success = true,
                    Data = ex
                };
            }

        }

        /// <summary>
        /// Cập nhật dữ liệu và DeviceUser
        /// </summary>
        /// <param name="ID">Id system</param>
        /// <param name="payload">Dữ liệu cần thay đổi</param>
        /// <returns>ID bản ghi nếu thành công/ MSG nếu thất bại</returns>
        [HttpPost("[action]/{ID}")]
        public ServiceResponse Update([FromRoute] Guid ID, [FromBody] DeviceUser payload)
        {
            try
            {
                if (payload==null)
                {
                    return new ServiceResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Success = false,
                        Data = "Đầu vào của device user đang rỗng"
                    };
                }
                else
                {
                    var check = ValidateRequestData(payload);
                    if (check.Success == false)
                    {
                        return new ServiceResponse
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Success = false,
                            Data = check.Data
                        };
                    }

                    if (ID != Guid.Empty)
                    {
                        DeviceUser DeviceUser = _context.DeviceUsers.FirstOrDefault(x => x.Id == ID);
                        if (DeviceUser == null)
                        {
                            return new ServiceResponse
                            {

                                StatusCode = StatusCodes.Status400BadRequest,
                                Success = false,
                                Data = "Không có bản ghi nào được tìm thấy"
                            };

                        }

                        DeviceUser.Id = ID;
                        DeviceUser.CustomerId = payload.CustomerId;
                        DeviceUser.DeviceId = payload.DeviceId;
                        DeviceUser.Status = payload.Status;
                        DeviceUser.AuthenType = payload.AuthenType;
                        DeviceUser.IssueDate = payload.IssueDate;
                        DeviceUser.CreatedDate = DateTime.Now;
                        DeviceUser.ApprovedDate = payload.ApprovedDate;
                        DeviceUser.DeletedDate = payload.DeletedDate;
                        DeviceUser.ActivedDate = payload.ActivedDate;
                        DeviceUser.ActiveCode = payload.ActiveCode;
                        DeviceUser.UserCreated = payload.UserCreated;
                        DeviceUser.UserDeleted = payload.UserDeleted;
                        DeviceUser.UserApproved = payload.UserApproved;

                        _context.SaveChanges();
                        return new ServiceResponse
                        {
                            StatusCode = StatusCodes.Status200OK,
                            Success = true,
                            Data = ID
                        };
                    }
                    else
                    {
                        return new ServiceResponse
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            Success = false,
                            Data = "ID system config không hợp lệ"
                        };
                    }
                }
               
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

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="ID">ID device user</param>
        /// <returns>ID bản ghi được xóa nếu thành công/ msg nếu thất bại</returns>
        [HttpPost("[action]/{ID}")]
        public ServiceResponse Remove([FromRoute] Guid ID)
        {
            try
            {
                DeviceUser DeviceUser = _context.DeviceUsers.Where(x => x.Id == ID).FirstOrDefault();
                if (DeviceUser == null)
                {
                    return new ServiceResponse
                    {

                        StatusCode = StatusCodes.Status400BadRequest,
                        Success = false,
                        Data = "Không có bản ghi nào được tìm thấy"
                    };

                }
                _context.DeviceUsers.Remove(DeviceUser);
                _context.SaveChanges();
                return new ServiceResponse
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = true,
                    Data = ID
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
