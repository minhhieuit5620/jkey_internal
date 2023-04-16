using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKEY_COMMON.Entities
{
    public class Bill
    {
        public int BillId { get; set; }
        public DateTimeOffset BillDate { get; set; }
        public DateTimeOffset BillDueDate { get; set; }
        public string BillName { get; set; }
        public int BillTypeId { get; set; }
        public int GoodsReceivedNoteId { get; set; }
        public string VendorDONumber { get; set; }
        public string VendorInvoiceNumber { get; set; }        
    }
}
