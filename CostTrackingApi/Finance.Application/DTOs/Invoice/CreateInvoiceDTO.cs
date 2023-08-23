using Finance.Application.DTOs.InvoiceItem;
using Finance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.DTOs.Invoice
{
    public class CreateInvoiceDTO
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public List<CreateInvoiceItemDTO> Items { get; set; }
        public int ConstructionSiteId { get; set; }
        public int MachineryId { get; set; }
        public int ToolId { get; set; }
        public int MaintenanceRecordId { get; set; }
        public int ArticleId { get; set; }
    }
}
