using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKEY_COMMON.Entities
{
    public class BaseEntities
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? DateCreated { get; set; } = DateTime.Now;

        /// <summary>
        /// Người tạo
        /// </summary>
        public Guid? UserCreated { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime? DateModified { get; set; } = DateTime.Now;

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public Guid? UserModified { get; set; } 
    }
}
