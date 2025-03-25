using CRUDTests.Practice_samples;

namespace CRUDTests
{
    public class OrdersTest
    {
        private readonly OrderService _orderService;

        // case 1 
        [Fact]
        public void CalculateTotal_ShouldReturnCorrectTotal_WhenGivenValidOrder()
        {
            // 1. Arrange - Declaration of variables and collecting Inputs.
            OrderService _orderService = new OrderService();
            var order_made = new Orders
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductName = "ProductA", Quantity = 1, Price = 30m },
                },
                Discount = 10
            };
            // 2. Act - Calling the method to be tested.
            decimal actual = _orderService.CalculateTotal(order_made);
            // 3. Assert - Comparing expected value with the actual value.
            Assert.Equal(29.70m,actual);
        }

        // case 2
        [Fact]
        public void CalculateTotal_ShouldThrowException_WhenOrderIsEmpty() {
            // 1. Arrange - Declaration of variables and collecting Inputs.
            var order = new Orders { Items = new List<OrderItem>(), Discount = 10 };
            OrderService _orderService = new OrderService();
            // 2. Act - Calling the method to be tested.
            // 3. Assert - Comparing expected value with the actual value.
            Assert.Throws<ArgumentException>(() => _orderService.CalculateTotal(order));
        }

        // case 3
        [Fact]
        public void CalculateTotal_ShouldApplyZeroDiscount_When_DiscountISZero() {
            // 1. Arrange - Declaration of variables and collecting Inputs.
            var order = new Orders { Items = new List<OrderItem> { 
                new OrderItem { ProductName="ProductA",Quantity=1,Price=100m} 
            }, Discount = 0 };

            //2. Act - Calling the method to be tested.
            OrderService _orderService = new OrderService();
            decimal actual_result = _orderService.CalculateTotal(order);

            //3.Assert - Comparing expected value with the actual value.
            Assert.Equal(110m,actual_result);

        }

        // case 4
        [Fact]
        public void CalculateTotal_ShouldThrowExceptionWhenPriceOrQuantityOrDiscountIsNegative()
        {
            // 1. Arrange - Declaration of variables and collecting Inputs.
            OrderService _orderService = new OrderService();

            var order1 = new Orders
            {
                Items = new List<OrderItem> {
                new OrderItem { ProductName="ProductA",Quantity=-1,Price=100m}
            }
            };

            // 2. Act and Assert
            Assert.Throws<ArgumentException>(()=>_orderService.CalculateTotal(order1));

            // 1. Arrange
            var order2 = new Orders
            {
                Items = new List<OrderItem> {
                new OrderItem { ProductName="ProductB",Quantity=1,Price=-100m}
            }
            };

            // 2. Act and Assert
            Assert.Throws<ArgumentException>(() => _orderService.CalculateTotal(order2));
        }

        [Fact]
        public void CalculateTotal_ShouldThrowException_When_DiscountIsOver100_Percent()
        {
           
            // 1. Arrange - Declaration of variables and collecting Inputs.
            OrderService _orderService = new OrderService();
            var order = new Orders
            {
                Items = new List<OrderItem> {
                new OrderItem { ProductName="ProductA",Quantity=1,Price=100m}
            },
                Discount = 121
            };

            // 2. Act and Assert
            Assert.Throws<ArgumentException>(() => _orderService.CalculateTotal(order));
        }
    }
}