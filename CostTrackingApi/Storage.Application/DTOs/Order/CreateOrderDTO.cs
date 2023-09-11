using Storage.Application.DTOs.OrderItem;
using Storage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.DTOs.Order
{
    public class CreateOrderDTO
    {

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public List<CreateOrderItemDTO> OrderItems { get; set; }

    }
}
