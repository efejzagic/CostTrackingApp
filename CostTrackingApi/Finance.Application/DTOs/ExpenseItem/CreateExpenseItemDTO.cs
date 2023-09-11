using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.DTOs.ExpenseItem
{
    public class CreateExpenseItemDTO
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int ExpenseId { get; set; }
    }
}
