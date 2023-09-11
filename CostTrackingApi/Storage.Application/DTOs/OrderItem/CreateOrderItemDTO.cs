using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.DTOs.OrderItem
{
    public  class CreateOrderItemDTO
    {

        public int ArticleId { get; set; }
        public int Quantity { get; set; }

        public double PricePerItem { get; set; }

        public int OrderId { get; set; }
    }
}
