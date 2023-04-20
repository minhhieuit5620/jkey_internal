using JKEY_INTERNAL.Models;
using JKEY_INTERNAL.Models.CRUD_Model;
using JKEY_INTERNAL.Models.CustomModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net;
using JKEY_INTERNAL.Models.Attributes;
using Microsoft.AspNetCore.Identity;

namespace JKEY_INTERNAL.Controllers
{
    [ApiController]
    [Route("api/SystemConfig")]
    public class SystemConfigController : BaseController<SystemConfig>
    {
        private readonly ILogger<SystemConfigController> _logger;
        private readonly JkeyInternalContext _context;


        public SystemConfigController(ILogger<SystemConfigController> logger, JkeyInternalContext dbContext)
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
        /// <returns>Danh sách system cofig theo thời gian tạo</returns>
        /// CreatedBy: HMHieu
        [HttpGet]
        public ServiceResponse GetAllSystem()
        {
            try
            {
                List<SystemConfig> listSystemConfig = _context.SystemConfigs.OrderByDescending(x => x.UserCreated).ToList();

                return new ServiceResponse
                {
                    StatusCode = StatusCodes.Status200OK,

                    Success = true,
                    Data = listSystemConfig
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
          [FromQuery] string? value="",// dữ liệu cần tìm kiếm        
          [FromQuery] string? name="",// dữ liệu cần tìm kiếm        
          [FromQuery] string? type  ="",// dữ liệu cần tìm kiếm        
          [FromQuery] int pageIndex=1,// trang hiện tại
          [FromQuery] int pageSize=15)//số bản ghi trong 1 trang
        {

            try
            {
                int skip = ((pageIndex - 1) * pageSize);
                var lst = _context.SystemConfigs.Where(x => x.Value.Contains(value) && x.Name.Contains(name) && x.Type.Contains(type)).OrderByDescending(y => y.DateCreated).ToList();
                
                if (pageSize != 0)
                {
                    var data = lst.Skip(skip).Take(pageSize);
                    int pages = lst.Count() % pageSize >= 1 ? lst.Count() / pageSize + 1 : lst.Count() / pageSize;
                    return new ServiceResponse 
                    { 
                        StatusCode=StatusCodes.Status200OK,
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
                    return new ServiceResponse {
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
        /// Lấy ra systemcofig theo Id
        /// </summary>
        /// <param name="ID">id system config</param>
        /// <returns>Bản ghi tương ứng với ID</returns>
        /// Created By: HMHieu
        [HttpGet("{ID}")]
        public ServiceResponse GetSysByID([FromRoute] Guid ID)
        {

            try
            {

                SystemConfig systemConfig = _context.SystemConfigs.Where(x => x.Id == ID).FirstOrDefault();

                if (systemConfig != null)
                {
                    return new ServiceResponse
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Success = true,
                        Data = systemConfig
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
        public ServiceResponse Insert([FromBody] SystemConfig payload)
        {
            try
            {
                if(payload == null)
                {
                    return new ServiceResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Success = false,
                        Data = "Dữ liệu đầu vào của system config đang rỗng"
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
                    SystemConfig systemConfig = new SystemConfig();
                    Guid id = Guid.NewGuid();
                    systemConfig.Id = id;
                    systemConfig.Description = payload.Description;
                    systemConfig.Name = payload.Name;
                    systemConfig.Value = payload.Value;
                    systemConfig.Type = payload.Type;
                    systemConfig.UserCreated = payload.UserCreated;
                    systemConfig.UserModified = payload.UserModified;
                    systemConfig.DateCreated = DateTime.Now;
                    systemConfig.DateModified = DateTime.Now;

                    _context.SystemConfigs.Add(systemConfig);
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
                    Success = false,
                    Data = ex
                };
              
            }

        }

        /// <summary>
        /// Cập nhật dữ liệu và SystemConfig
        /// </summary>
        /// <param name="ID">Id system</param>
        /// <param name="payload">Dữ liệu cần thay đổi</param>
        /// <returns>ID bản ghi nếu thành công/ MSG nếu thất bại</returns>
        [HttpPost("[action]/{ID}")]
        public ServiceResponse Update([FromRoute] Guid ID, [FromBody] SystemConfig payload)
        {
            try
            {
               
                if (payload == null)
                {
                    return new ServiceResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Success = false,
                        Data = "Dữ liệu đầu vào của system config đang rỗng"
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
                        SystemConfig systemConfig = _context.SystemConfigs.FirstOrDefault(x => x.Id == ID);
                        if (systemConfig == null)
                        {
                            return new ServiceResponse
                            {

                                StatusCode = StatusCodes.Status400BadRequest,
                                Success = false,
                                Data = "Không có bản ghi nào được tìm thấy"
                            };

                        }

                        systemConfig.Id = ID;
                        systemConfig.Description = payload.Description;
                        systemConfig.Name = payload.Name;
                        systemConfig.Value = payload.Value;
                        systemConfig.Type = payload.Type;
                        systemConfig.UserCreated = payload.UserCreated;
                        systemConfig.UserModified = payload.UserModified;
                        systemConfig.DateCreated = payload.DateCreated;
                        systemConfig.DateModified = DateTime.Now;

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
        /// <param name="ID">ID system</param>
        /// <returns>ID bản ghi được xóa nếu thành công/ msg nếu thất bại</returns>
        [HttpPost("[action]/{ID}")]
        public ServiceResponse Remove([FromRoute] Guid ID)
        {
            try
            {
                SystemConfig systemConfig = _context.SystemConfigs.Where(x => x.Id == ID).FirstOrDefault();
                if (systemConfig == null)
                {
                    return new ServiceResponse
                    {

                        StatusCode = StatusCodes.Status400BadRequest,
                        Success = false,
                        Data = "Không có bản ghi nào được tìm thấy"
                    };

                }
                _context.SystemConfigs.Remove(systemConfig);
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
