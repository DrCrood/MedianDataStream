using Moq;
using System;
using Xunit;
using MedianDataStream;

namespace MedianDataStreamTest
{
    public class MinHeapTests
    {
        private readonly int HEAP_SIZE = 5;
        private MinHeap CreateMinHeap()
        {
            return new MinHeap(HEAP_SIZE);
        }

        [Theory]
        [InlineData(new int[] { 3 }, 0, 3)]
        [InlineData(new int[] { 3, 2, 6, 4 }, 1, 2)]
        [InlineData(new int[] { 4, 8, 6, 9, 7, 6, 4, 8, 2, 10, 6, 12, 13, 7, 9, 12, 11 }, 8, 2)]
        public void Push_ValidInputs_ShouldReturnCorrectMinValue(int[] data, int index, int value)
        {
            // Arrange
            var minHeap = this.CreateMinHeap();

            // Act
            int size = data.Length;
            for(int i = 0; i < size; i++)
            {
                minHeap.Push(i, data[i]);
            }

            // Assert
            Assert.Equal((index, value), minHeap.Pop());
        }

        [Fact]
        public void Pop_ValidInput_ShouldReturnMinValuesInOrder()
        {
            // Arrange
            var minHeap = this.CreateMinHeap();
            int[] data = new int[] { 12, 13, 7, 9, 12, 11, 8, 10, 6, 18, 20, 10 };
            // Act
            int size = data.Length;
            for (int i = 0; i < size; i++)
            {
                minHeap.Push(i, data[i]);
            }

            // Assert
            Assert.Equal((8, 6), minHeap.Pop());
            Assert.Equal((2, 7), minHeap.Pop());
        }

        [Fact]
        public void Remove_RemoveData_ShouldRemoveDataInCorrectIndex()
        {
            // Arrange
            var minHeap = this.CreateMinHeap();

            int[] data = new int[] { 12, 10, 7, 9, 12, 11, 8, 10, 6, 18, 20, 10 };
            // Act
            int size = data.Length;
            for (int i = 0; i < size; i++)
            {
                minHeap.Push(i, data[i]);
            }

            minHeap.Remove(3);
            minHeap.Remove(8);
            // Assert
            Assert.Equal((2, 7), minHeap.Pop());
            Assert.Equal((6, 8), minHeap.Pop());
        }

        [Fact]
        public void Count_ValidInput_ShouldRetuenCorrectHeapCount()
        {
            // Arrange
            var minHeap = this.CreateMinHeap();
            int[] data = new int[] { 12, 10, 7, 9, 12, 11, 8, 10, 6, 18, 20, 10 };
            // Act
            int size = data.Length;
            for (int i = 0; i < size; i++)
            {
                minHeap.Push(i, data[i]);
            }

            // Assert
            Assert.Equal(12, minHeap.Count());
        }

        [Fact]
        public void ContainsIndex_ValidInput_ShouldRetuenTrueIfContainsTheIndex()
        {
            // Arrange
            var minHeap = this.CreateMinHeap();
            int[] data = new int[] { 12, 10, 7, 9, 6, 18, 20, 10 };

            // Act
            int size = data.Length;
            for (int i = 0; i < size; i++)
            {
                minHeap.Push(i, data[i]);
            }

            // Assert
            Assert.True(minHeap.ContainsIndex(7));
        }

        [Fact]
        public void ContainsIndex_ValidInput_ShouldRetuenFalseIfNotContainTheIndex()
        {
            // Arrange
            var minHeap = this.CreateMinHeap();
            int[] data = new int[] { 12, 10, 7, 9, 6, 18, 20, 10 };

            // Act
            int size = data.Length;
            for (int i = 0; i < size; i++)
            {
                minHeap.Push(i, data[i]);
            }

            // Assert
            Assert.False(minHeap.ContainsIndex(8));
        }

    }
}
