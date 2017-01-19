/*
    Grant McDade
    Copyright(C) 2017 Grant McDade

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.If not, see<http://www.gnu.org/licenses/>.
*/

using ProjectEuler.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEulerWorkbench.Problems
{
    class Problem998 : IProblem
    {
        public string Description
        {
            get { return "Heap implementation and testing"; }
        }

        public string Solve()
        {
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();

            List<int> data = new List<int>() { 4, 1, 3, 2, 16, 9, 10, 14, 8, 7, 5, 6, 11, 12, 13, 15 };

            //var limit = 10000000;
            //var data = new List<int>(limit);
            //var random = new Random();
            //for (int i = 0; i < limit; ++i)
            //{
            //    data.Add(random.Next(limit));
            //}

            var buildArray = stopWatch.ElapsedMilliseconds;

            // Build a max heap from the array
            var heap = new Heap<int>(data);
            // Heapsort(data);

            var buildMaxHeap = stopWatch.ElapsedMilliseconds - buildArray;

            if (!heap.Validate())
                throw new Exception("The heap is invalid!");

            heap.IncreaseKey(5, heap.Data[5] + 1);

            if (!heap.Validate())
                throw new Exception("The heap is invalid!");

            heap.Insert(11);

            if (!heap.Validate())
                throw new Exception("The heap is invalid!");

            heap.Insert(11);

            if (!heap.Validate())
                throw new Exception("The heap is invalid!");

            heap.Insert(11);

            if (!heap.Validate())
                throw new Exception("The heap is invalid!");

            while (heap.Size > 0)
            {
                var max = heap.ExtractMax();
            }

            return String.Format("Array: {0}ms, Heap: {1}ms", buildArray, buildMaxHeap);
        }

    //    private static int Parent(int index)
    //    {
    //        return ((index + 1) / 2) - 1;
    //    }

    //    private static int Left(int index)
    //    {
    //        return (index + 1) * 2 - 1;
    //    }

    //    private static int Right(int index)
    //    {
    //        return (index + 1) * 2;
    //    }

    //    private static void MaxHeapify<T>(List<T> data, int index)
    //    {
    //        MaxHeapify(data, index, data.Count);
    //    }

    //    private static void MaxHeapify<T>(List<T> data, int index, int heapSize)
    //    {
    //        int leftIndex = Left(index);
    //        int rightIndex = Right(index);
    //        IComparer<T> comparer = Comparer<T>.Default;

    //        int largest = index;
    //        if (leftIndex < heapSize && comparer.Compare(data[leftIndex], data[index]) > 0)
    //        {
    //            largest = leftIndex;
    //        }

    //        if (rightIndex < heapSize && comparer.Compare(data[rightIndex], data[largest]) > 0)
    //        {
    //            largest = rightIndex;
    //        }

    //        if (largest != index)
    //        {
    //            T temp = data[largest];
    //            data[largest] = data[index];
    //            data[index] = temp;
    //            MaxHeapify(data, largest, heapSize);
    //        }
    //    }

    //    private static void BuildMaxHeap<T>(List<T> data)
    //    {
    //        for (int i = (data.Count / 2) - 1; i >= 0; --i)
    //        {
    //            MaxHeapify(data, i);
    //        }
    //    }

    //    private static void Heapsort<T>(List<T> data)
    //    {
    //        BuildMaxHeap(data);
    //        var heapSize = data.Count;
    //        for (int i = data.Count - 1; i >= 0; --i)
    //        {
    //            T temp = data[i];
    //            data[i] = data[0];
    //            data[0] = temp;
    //            --heapSize;
    //            MaxHeapify(data, 0, heapSize);
    //        }
    //    }

    //    private static T HeapMax<T>(List<T> data)
    //    {
    //        return data[0];
    //    }

    //    private static T HeapExtractMax<T>(List<T> data)
    //    {
    //        var max = data[0];

    //        data[0] = data[data.Count - 1];
    //        MaxHeapify(data, 0);

    //        return max;
    //    }

    //    private static void HeapIncreaseKey<T>(List<T> data, int index, T key)
    //    {
    //        var comparer = Comparer<T>.Default;
    //        if (comparer.Compare(key, data[index]) < 0)
    //            throw new Exception("The new key may not be smaller than the current key");

    //        data[index] = key;
    //        var parentIndex = Parent(index);
    //        while (index > 0 && comparer.Compare(data[parentIndex], data[index]) < 0)
    //        {
    //            T temp = data[index];
    //            data[index] = data[parentIndex];
    //            data[parentIndex] = temp;

    //            index = parentIndex;
    //            parentIndex = Parent(index);
    //        }
    //    }

    //    private static void MaxHeapInsert<T>(List<T> data, T key)
    //    {
    //        T smallest = data[data.Count - 1];
    //        if (Comparer<T>.Default.Compare(key, smallest) < 0)
    //            smallest = key;
    //        data.Add(smallest);
    //        HeapIncreaseKey(data, data.Count - 1, key);
    //    }

    //    private static bool ValidateHeap<T>(List<T> data, IComparer<T> comparer, int index)
    //    {
    //        // If we get an invalid index then we have reached the end of the heap
    //        // and can consider it valid
    //        if (index >= data.Count)
    //            return true;

    //        if (comparer == null)
    //            comparer = Comparer<T>.Default;

    //        // Recursively verify that both the left and right
    //        // children are always smaller than the parent
    //        int left = (index + 1) * 2 - 1;
    //        int right = (index + 1) * 2;

    //        if (left < data.Count && comparer.Compare(data[left], data[index]) > 0)
    //            return false;

    //        if (right < data.Count && comparer.Compare(data[right], data[index]) > 0)
    //            return false;

    //        return ValidateHeap(data, comparer, left) && ValidateHeap(data, comparer, right);
    //    }

    }
}
