namespace CRUDTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // step 1: Arrange
               MyMath mm = new MyMath();
               int input1 = 20,input2 = -10;
               int expectedValue = 10;
            // step 2: Act
               int actualReturnedValue = mm.AddNumbers(input1,input2);
            // step 3: Assert
            Assert.Equal(expectedValue,actualReturnedValue);
        }

        [Fact]
        public void Test2()
        {
            MyMath mm = new MyMath();
            var expectedOutput = new {
                name = "stark",
                age = 12,
                gender = "M"
            };
            var actualOutput = mm.HardCodedValue();
            Assert.Equal(expectedOutput,actualOutput);
        }
    }
}