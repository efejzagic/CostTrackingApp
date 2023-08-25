using Finance.Application.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.DTOs.InvoiceItem
{
    public  class InvoiceItemDTO : RequestParameter
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int InvoiceId { get; set; } 
    }
}
