

namespace JKEY_INTERNAL.Models.CustomModel
{
    public class ServiceResponse
    {

     
        public int StatusCode { get; set; }
        /// <summary>
        /// Thành công hay thất bại
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Dữ liệu đi kèm khi thành công hoặc thất bại
        /// </summary>
        public object Data { get; set; }
    }
}
