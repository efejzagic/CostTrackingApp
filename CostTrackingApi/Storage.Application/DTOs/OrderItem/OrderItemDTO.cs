using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.DTOs.OrderItem
{
    public class OrderItemDTO
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }
        public int Quantity { get; set; }

        public double PricePerItem { get; set; }

        public int OrderId { get; set; }
    }
}
