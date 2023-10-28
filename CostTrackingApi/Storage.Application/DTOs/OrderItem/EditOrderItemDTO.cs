using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.DTOs.OrderItem
{
    public class EditOrderItemDTO
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }
        public int Quantity { get; set; }
        public string ArticleName { get; set; }
        public double PricePerItem { get; set; }

        public int OrderId { get; set; }
    }
}
