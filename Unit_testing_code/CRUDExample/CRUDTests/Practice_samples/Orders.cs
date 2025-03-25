using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTests.Practice_samples
{
    // DTO
    public class OrderItem {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    // DTO
    public class Orders
    {
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal Discount { get; set; } // Discount in percentage
    }

    public class OrderService
    {
        public const decimal TaxRate = 0.1m; // 10% tax
        public decimal CalculateTotal(Orders order)
        {
            if (order.Items == null || !order.Items.Any())
            {
                throw new ArgumentException("Order must contain at least one item.");
            }

            if (order.Items.Any(item => item.Quantity < 0)) {
                throw new ArgumentException("Quantity cannot be negative.");
            }

            if (order.Items.Any(item => item.Price < 0)) {
                throw new ArgumentException("Product price cannot be negative.");
            }

            if (order.Discount>100) {
                throw new ArgumentException("Discount cannot be over 100 %");
            }

        decimal subtotal = order.Items.Sum(item => item.Price * item.Quantity);
            decimal discountAmount = (order.Discount / 100) * subtotal;
            decimal totalAfterDiscount = subtotal - discountAmount;
            decimal taxAmount = totalAfterDiscount * TaxRate;

            return totalAfterDiscount + taxAmount;
        }
    }
}
