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
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public string? ModifiedBy { get; set; } 
    }
}
