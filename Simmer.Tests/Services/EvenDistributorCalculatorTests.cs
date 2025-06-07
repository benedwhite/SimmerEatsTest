using SimmerInterviewTask.Shared.Services;
using Xunit;

namespace Simmer.Tests.Services
{
    public class EvenDistributorCalculatorTests
    {
        private readonly EvenDistributorCalculator _sut = new();

        [Theory]
        [InlineData(10, 3, 3)]
        [InlineData(9, 3, 3)]
        [InlineData(8, 3, 2)]
        [InlineData(0, 3, 0)]
        public void CalculateMinimumNumber_ReturnsExpected(int count, int itemCount, int expected)
        {
            // Arrange & Act
            var result = _sut.CalculateMinimumNumber(count, itemCount);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 3, 3, 1)]
        [InlineData(9, 3, 3, 0)]
        [InlineData(8, 2, 4, 0)]
        [InlineData(7, 2, 3, 1)]
        [InlineData(0, 3, 0, 0)]
        public void CalculateLeftOver_ReturnsExpected(int count, int minimumNumber, int itemCount, int expected)
        {
            // Arrange & Act
            var result = _sut.CalculateLeftOver(count, minimumNumber, itemCount);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 3, 1, 4)]
        [InlineData(1, 3, 1, 3)]
        [InlineData(2, 3, 1, 3)]
        [InlineData(0, 2, 2, 3)]
        [InlineData(1, 2, 2, 3)]
        [InlineData(2, 2, 2, 2)]
        public void CalculateQuantity_ReturnsExpected(int index, int minimumNumber, int leftOver, int expected)
        {
            // Arrange & Act
            var result = _sut.CalculateQuantity(index, minimumNumber, leftOver);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
