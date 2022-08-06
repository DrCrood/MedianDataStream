using System;
using System.Collections.Generic;

namespace MedianDataStream
{
    /// <summary>
    /// Max heap with track of the data index so it can be removed effciently from heap.
    /// </summary>
    public class MaxHeap
    {
        private int size;
        private int heapSize;
        private int[] data;
        private readonly Dictionary<int, int> array2HeapMap;
        private readonly Dictionary<int,int> heap2ArrayMap;
        public MaxHeap(int size)
        {
            this.size = size;
            data = new int[size];
            array2HeapMap = new Dictionary<int, int>();
            heap2ArrayMap = new Dictionary<int, int>();
        }

        private void SizeIncrease()
        {
            int s = this.size;
            this.size *= 2;
            int[] narr = new int[this.size];
            for (int i = 0; i < s; i++)
            {
                narr[i] = data[i];
            }
            data = narr;
        }

        /// <summary>
        /// Push a number and its index to the Heap. The index is used for remove the num from the Heap later on.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="index"></param>
        public void Push(int index, int num)
        {
            if (heapSize + 1 > size)
            {
                this.SizeIncrease();
            }
            data[heapSize] = num;
            array2HeapMap[index] = heapSize;
            heap2ArrayMap[heapSize] = index;
            int i = heapSize;
            while (i >= 0 && data[Parent(i)] < data[i])
            {
                int j = Parent(i);
                (data[j], data[i]) = (data[i], data[j]);
                UpdateIndexMap(i ,j);
                i = j;
            }
            heapSize++;
        }

        public (int, int) Pop()
        {
            int max = data[0];
            int index = heap2ArrayMap[0];
            data[0] = data[heapSize - 1];
            array2HeapMap.Remove(index);
            heap2ArrayMap.Remove(0);

            if (heapSize == 1)
            {
                heapSize--;
                return (index, max);
            }

            int k = heap2ArrayMap[heapSize - 1];
            array2HeapMap[k] = 0;
            heap2ArrayMap[0] = k;
            heap2ArrayMap.Remove(heapSize - 1);
            heapSize--;
            MaxHeapify(0);
            return (index, max);
        }

        private void UpdateIndexMap(int i, int j)
        {
            int indexi = heap2ArrayMap[i];
            int indexj = heap2ArrayMap[j];
            (heap2ArrayMap[j], heap2ArrayMap[i]) = (heap2ArrayMap[i], heap2ArrayMap[j]);
            (array2HeapMap[indexi], array2HeapMap[indexj]) = (array2HeapMap[indexj], array2HeapMap[indexi]);
        }

        public void Remove(int index)
        {
            int i = array2HeapMap[index];
            array2HeapMap.Remove(index);

            data[i] = data[heapSize - 1];
            int j = heap2ArrayMap[heapSize-1];
            heap2ArrayMap[i] = j;
            array2HeapMap[j] = i;
            heap2ArrayMap.Remove(heapSize - 1);

            heapSize--;
            MaxHeapify(i);
        }

        public int Count()
        {
            return this.heapSize;
        }
        public bool ContainsIndex(int i)
        {
            return array2HeapMap.ContainsKey(i);
        }

        private void MaxHeapify(int index)
        {
            int left = 2 * index;
            int right = 2 * index + 1;
            int maxIndex = (left < heapSize && data[left] > data[index]) ? left : index;
            maxIndex = (right < heapSize && data[right] > data[maxIndex]) ? right : maxIndex;

            if (maxIndex != index)
            {
                (data[maxIndex], data[index]) = (data[index], data[maxIndex]);
                UpdateIndexMap(maxIndex, index);
                MaxHeapify(maxIndex);
            }
        }

        private static int Parent(int i)
        {
            double k = i / 2;
            return (int)Math.Floor(k);
        }

        public int Peek()
        {
            return data[0];
        }
    }
}
