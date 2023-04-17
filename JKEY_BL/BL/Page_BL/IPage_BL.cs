using JKEY_COMMON.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKEY_BL.BL.Page_BL
{
    public interface IPage_BL
    {
        public IEnumerable<Page> GetAllPage();
    }
}
