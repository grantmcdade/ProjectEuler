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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ProjectEuler.Library
{
    public class Heap<T>
    {
        private IComparer<T> _comparer;
        private List<T> _data;

        public ReadOnlyCollection<T> Data
        {
            get { return _data.AsReadOnly(); }
        }

        public int Size
        {
            get { return _data.Count - 1; }
        }

        private void MaxHeapify(int index)
        {
            MaxHeapify(index, Size);
        }

        private void MaxHeapify(int index, int heapSize)
        {
            int leftIndex = index * 2;
            int rightIndex = index * 2 + 1;

            int largest = index;
            if (leftIndex <= heapSize && _comparer.Compare(_data[leftIndex], _data[index]) > 0)
            {
                largest = leftIndex;
            }

            if (rightIndex <= heapSize && _comparer.Compare(_data[rightIndex], _data[largest]) > 0)
            {
                largest = rightIndex;
            }

            if (largest != index)
            {
                T temp = _data[largest];
                _data[largest] = _data[index];
                _data[index] = temp;
                MaxHeapify(largest, heapSize);
            }
        }

        private void BuildMaxHeap()
        {
            for (int i = _data.Count / 2; i > 0; --i)
            {
                MaxHeapify(i);
            }
        }

        //private static void Heapsort<T>(List<T> data)
        //{
        //    BuildMaxHeap(data);
        //    var heapSize = data.Count;
        //    for (int i = data.Count - 1; i >= 0; --i)
        //    {
        //        T temp = data[i];
        //        data[i] = data[0];
        //        data[0] = temp;
        //        --heapSize;
        //        MaxHeapify(data, 0, heapSize);
        //    }
        //}

        public Heap()
        {
            _data = new List<T>() { default(T) };
            _comparer = Comparer<T>.Default;
        }

        public Heap(IComparer<T> comparer)
        {
            _data = new List<T>() { default(T) };
            _comparer = comparer;
        }

        public Heap(List<T> data)
        {
            _data = new List<T>() { default(T) };
            _data.AddRange(data);
            _comparer = Comparer<T>.Default;
            BuildMaxHeap();
        }

        public Heap(List<T> data, IComparer<T> comparer)
        {
            _data = new List<T>() { default(T) };
            _data.AddRange(data);
            _comparer = comparer;
            BuildMaxHeap();
        }

        public T Max()
        {
            return _data[1];
        }

        public T ExtractMax()
        {
            var max = _data[1];

            _data[1] = _data[Size];
            _data.RemoveAt(Size);
            MaxHeapify(1);

            return max;
        }

        public void IncreaseKey(int index, T key)
        {
            if (_comparer.Compare(key, _data[index]) < 0)
                throw new Exception("The new key may not be smaller than the current key");

            _data[index] = key;
            var parentIndex = index / 2;
            while (index > 1 && _comparer.Compare(_data[parentIndex], _data[index]) < 0)
            {
                T temp = _data[index];
                _data[index] = _data[parentIndex];
                _data[parentIndex] = temp;

                index = parentIndex;
                parentIndex = index / 2;
            }
        }

        public void Insert(T key)
        {
            T smallest = _data[Size];
            if (_comparer.Compare(key, smallest) < 0)
                smallest = key;
            _data.Add(smallest);
            IncreaseKey(Size, key);
        }

        public bool Validate()
        {
            if (_comparer.Compare(_data[0], default(T)) != 0)
                return false;

            return Validate(1);
        }

        private bool Validate(int index)
        {
            // If we get an invalid index then we have reached the end of the heap
            // and can consider it valid
            if (index >= _data.Count)
                return true;

            // Recursively verify that both the left and right
            // children are always smaller than the parent
            int left = index * 2;
            int right = index * 2 + 1;

            if (left < _data.Count && _comparer.Compare(_data[left], _data[index]) > 0)
                return false;

            if (right < _data.Count && _comparer.Compare(_data[right], _data[index]) > 0)
                return false;

            return Validate(left) && Validate(right);
        }

    }
}
