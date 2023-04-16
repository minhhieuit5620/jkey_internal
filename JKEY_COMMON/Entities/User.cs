using JKEY_COMMON.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKEY_COMMON.Entities
{
    public class User:BaseEntities
    {
        /// <summary>
        /// ID người dùng
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Tên người dùng
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// email người dùng
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// giới tính user
        /// </summary>
        public gender Gender { get; set; }

        /// <summary>
        /// Ngày sinh user
        /// </summary>
        public DateTime? DOB { get; set; }

        /// <summary>
        /// Tên đăng nhập user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// mật khẩu user
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// Trạng thái user
        /// </summary>
        public Active Active { get; set; }
      
    }
}
