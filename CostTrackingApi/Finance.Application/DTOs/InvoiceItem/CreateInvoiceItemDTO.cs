using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.DTOs.InvoiceItem
{
    public class CreateInvoiceItemDTO
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int InvoiceId { get; set; }
    }
}
