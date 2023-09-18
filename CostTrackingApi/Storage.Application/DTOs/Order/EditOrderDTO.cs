using Storage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.DTOs.Order
{
    public class EditOrderDTO
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime ShippingDate { get; set; }

        public bool OrderComplete { get; set; }


        public double TotalAmount { get; set; }

        public List<Domain.Entities.OrderItem> OrderItems { get; set; }

    }
}
