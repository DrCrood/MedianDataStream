using Xunit;
using MedianDataStream;

namespace MedianDataStreamTest
{
    public class MedianStreamFWTests
    {
        private readonly int WINDOW_SIZE = 10;
        private MedianStreamFW CreateMedianStreamFW()
        {
            return new MedianStreamFW(WINDOW_SIZE);
        }

        [Theory]
        [InlineData(new int[] { 3 }, 3)]
        [InlineData(new int[] { 3, 3, 6, 4 }, 3.5)]
        [InlineData(new int[] { 3, 4, 5, 1, 2, 3, 4 }, 3)]
        [InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 5.5)]
        [InlineData(new int[] { 5, 4, 6, 6, 7, 3, 2, 5, 6, 9, 3, 2, 11, 11, 10, 6, 7 }, 6.5)]
        [InlineData(new int[] {4, 2, 6, 9, 7, 6, 4, 8, 2, 10, 6, 12, 13, 7, 9, 12, 11, 8, 10, 6, 18, 20, 10 }, 10)]
        public void AddGet_ValidInputs_ShouldReturnCorrectMedianValue(int[] data, double median)
        {
            // Arrange
            var medianStreamFW = this.CreateMedianStreamFW();

            // Act
            foreach (int i in data)
            {
                medianStreamFW.Add(i);
            }

            // Assert
            Assert.Equal(median, medianStreamFW.Get());
        }
    }
}
