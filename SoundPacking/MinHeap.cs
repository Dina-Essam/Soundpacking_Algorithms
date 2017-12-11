using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPacking
{
    class MinHeap
    {
        public static void HeapSort(AudioFile[] input)
        {
            int heapSize = input.Length;
            for (int p = heapSize / 2 - 1; p >= 0; p--)
                MinHeapify(input, heapSize, p);

            for (int i = input.Length - 1; i >= 0; i--)
            {
                swap(input, i, 0);
                heapSize--;
                MinHeapify(input, heapSize, 0);
            }
        }

        private static void MinHeapify(AudioFile[] input, int heapSize, int index)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int smallest = index;

            if (left < heapSize && input[left].Duration.TotalSeconds < input[index].Duration.TotalSeconds)
                smallest = left;

            if (right < heapSize && input[right].Duration.TotalSeconds < input[smallest].Duration.TotalSeconds)
                smallest = right;

            if (smallest != index)
            {
                swap(input, index, smallest);
                MinHeapify(input, heapSize, smallest);
            }
        }

        private static void swap(AudioFile[] arr, int i, int j)
        {
            AudioFile temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
