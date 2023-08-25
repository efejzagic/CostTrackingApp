using Finance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.DTOs.Expense
{
    public class EditExpenseDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public ExpenseType Type { get; set; }
        public int ReferenceId { get; set; }

        // Navigation properties
        public int ConstructionSiteId { get; set; }
        public int MachineryId { get; set; }
        public int ToolId { get; set; }
        public int MaintenanceRecordId { get; set; }
        public int ArticleId { get; set; }
    }
}
