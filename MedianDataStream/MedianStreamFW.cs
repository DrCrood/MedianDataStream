using System;

namespace MedianDataStream
{
    /// <summary>
    /// A median datastream class using fixed size sliding window to get the median value.
    /// </summary>
    public class MedianStreamFW
    {
        private readonly int size;
        private int count = 0;
        private int index = 0;
        private double median;
        private readonly MinHeap minHeap;
        private readonly MaxHeap maxHeap;
        private readonly int[] data;
        public MedianStreamFW(int size)
        {
            this.size = size;
            data = new int[size];
            minHeap = new MinHeap(size/2 + 1);
            maxHeap = new MaxHeap(size/2 + 1);
            for(int i = 0; i < size; i++)
            {
                data[i] = int.MinValue;
            }
        }
        /// <summary>
        /// Add a new data to the window and update the median value.
        /// Will first remove the number which just move out of the window.
        /// </summary>
        /// <param name="number"></param>
        public void Add(int number)
        {
            if(index < size)
            {
                Remove(index, data[index]);
            }
            else
            {
                Remove(0, data[0]);
                index = 0;
            }

            data[index] = number;
            if(number >= median)
            {
                minHeap.Push(index, number);
            }
            else
            {
                maxHeap.Push(index, number);
                (int i, int num) = maxHeap.Pop();
                minHeap.Push(i, num);
            }

            if(minHeap.Count() > maxHeap.Count())
            {
                (int index, int num) = minHeap.Pop();
                maxHeap.Push(index, num);
            }
           
            if(count < size) { count++; }
            if (count % 2 == 0)
            {
                this.median = (minHeap.Peek() + maxHeap.Peek()) / 2.0;
            }
            else
            {
                this.median = maxHeap.Peek();
            }
            index++;
        }

        /// <summary>
        /// Remove the data from heaps using its index in the original data array.
        /// </summary>
        /// <param name="indexi"></param>
        /// <param name="value"></param>
        private void Remove(int indexi, int value)
        {
            if(value == Int32.MinValue)
            {
                return;  //nothing to remove from the data stream
            }

            if(minHeap.ContainsIndex(indexi))
            {
                minHeap.Remove(indexi);
            }
            else
            {
                maxHeap.Remove(indexi);
            }

            if (minHeap.Count() > maxHeap.Count())
            {
                (int index, int num) = minHeap.Pop();
                maxHeap.Push(index, num);
            }
            count--;
            if (count % 2 == 0)
            {
                this.median = (minHeap.Peek() + maxHeap.Peek()) / 2.0;
            }
            else
            {
                this.median = maxHeap.Peek();
            }
        }

        /// <summary>
        /// Return the median value.
        /// </summary>
        /// <returns></returns>
        public double Get()
        {
            return this.median;
        }
    }
}
