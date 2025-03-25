namespace CRUDTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // 1. Arrange - Declaration of variables and collecting Inputs.
            MyMath mm = new MyMath();
            int num1 = 100;
            int num2 = 200;
            int expected = 300;
            // 2. Act - Calling the method to be tested.
            int actual = mm.Add(num1, num2);
            // 3. Assert - Comparing expected value with the actual value.
            actual.Equals(expected);
        }
    }
}