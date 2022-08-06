using Moq;
using System;
using Xunit;
using MedianDataStream;

namespace MedianDataStreamTest
{
    public class MaxHeapTests
    {
        private readonly int HEAP_SIZE = 5;
        private MaxHeap CreateMaxHeap()
        {
            return new MaxHeap(HEAP_SIZE);
        }

        [Theory]
        [InlineData(new int[] { 3 }, 0, 3)]
        [InlineData(new int[] { 3, 2, 6, 4 }, 2, 6)]
        [InlineData(new int[] { 4, 8, 6, 9, 17, 6, 4, 8, 2, 10, 6, 12, 13, 7, 9, 12, 11 }, 4, 17)]
        public void Push_ValidInputs_ShouldReturnCorrectMinValue(int[] data, int index, int value)
        {
            // Arrange
            var maxHeap = this.CreateMaxHeap();

            // Act
            int size = data.Length;
            for (int i = 0; i < size; i++)
            {
                maxHeap.Push(i, data[i]);
            }

            // Assert
            Assert.Equal((index, value), maxHeap.Pop());
        }

        [Fact]
        public void Pop_ValidInput_ShouldReturnMaxValuesInOrder()
        {
            // Arrange
            var maxHeap = this.CreateMaxHeap();
            int[] data = new int[] { 12, 13, 7, 9, 12, 18, 8, 10, 6, 8, 20, 10 };
            // Act
            int size = data.Length;
            for (int i = 0; i < size; i++)
            {
                maxHeap.Push(i, data[i]);
            }

            // Assert
            Assert.Equal((10, 20), maxHeap.Pop());
            Assert.Equal((5, 18), maxHeap.Pop());
        }

        [Fact]
        public void Remove_RemoveData_ShouldRemoveDataInCorrectIndex()
        {
            // Arrange
            var maxHeap = this.CreateMaxHeap();

            int[] data = new int[] { 6, 16, 7, 19, 12, 17, 10, 8, 20, 10 };
            // Act
            int size = data.Length;
            for (int i = 0; i < size; i++)
            {
                maxHeap.Push(i, data[i]);
            }

            maxHeap.Remove(3);
            maxHeap.Remove(5);
            // Assert
            Assert.Equal((8, 20), maxHeap.Pop());
            Assert.Equal((1, 16), maxHeap.Pop());
        }

        [Fact]
        public void Count_ValidInput_ShouldRetuenCorrectHeapCount()
        {
            // Arrange
            var maxHeap = this.CreateMaxHeap();
            int[] data = new int[] { 12, 10, 7, 9, 12, 11, 8, 10, 6, 18, 20, 10 };
            // Act
            int size = data.Length;
            for (int i = 0; i < size; i++)
            {
                maxHeap.Push(i, data[i]);
            }

            // Assert
            Assert.Equal(12, maxHeap.Count());
        }

        [Fact]
        public void ContainsIndex_ValidInput_ShouldRetuenTrueIfContainsTheIndex()
        {
            // Arrange
            var maxHeap = this.CreateMaxHeap();
            int[] data = new int[] { 12, 10, 7, 9, 6, 18, 20, 10 };

            // Act
            int size = data.Length;
            for (int i = 0; i < size; i++)
            {
                maxHeap.Push(i, data[i]);
            }

            // Assert
            Assert.True(maxHeap.ContainsIndex(7));
        }

        [Fact]
        public void ContainsIndex_ValidInput_ShouldRetuenFalseIfNotContainTheIndex()
        {
            // Arrange
            var maxHeap = this.CreateMaxHeap();
            int[] data = new int[] { 12, 10, 7, 9, 6, 18, 20, 10 };

            // Act
            int size = data.Length;
            for (int i = 0; i < size; i++)
            {
                maxHeap.Push(i, data[i]);
            }

            // Assert
            Assert.False(maxHeap.ContainsIndex(8));
        }

    }
}
