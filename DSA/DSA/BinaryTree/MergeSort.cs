using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DSA.BinaryTree
{
    public class MergeSort<T> (T[] _array) where T : IComparable<T>
    {
        public T[] Sort()
        {
          return Sort(_array);
            //SortInPlace(_array, 0, _array.Length - 1);
        }
        public T[] Sort(T[] array) // duplicate array
        {
            if (array.Length <= 1)
            {
                //Console.WriteLine(array[0]!.ToString());
                return array;
            }
            T[] left = new T[array.Length / 2];
            T[] right = new T[array.Length - left.Length];

            Array.Copy(array, 0, left, 0, left.Length);
            Array.Copy(array, left.Length, right, 0, right.Length);
                     
            return Merge(Sort(left), Sort(right));
        }
        private T[] Merge(T[] left, T[] right) 
        {
            //Console.WriteLine($"Left: {string.Join(',', left)}: Right: {string.Join(',', right)}");
            T[] result = new T[left.Length + right.Length];
            
            int leftIndex = 0;
            int rightIndex = 0;
            for (int resultIndex = 0; resultIndex < result.Length; resultIndex++)
            {
                if (leftIndex>=left.Length)
                {
                    result[resultIndex] = right[rightIndex];
                    rightIndex++;
                }
                else if (rightIndex >= right.Length)
                {
                    result[resultIndex] = left[leftIndex];
                    leftIndex++;
                }
                else if (left[leftIndex].CompareTo(right[rightIndex]) < 0)
                {
                    result[resultIndex] = left[leftIndex];
                    leftIndex++;
                }
                else
                {
                    result[resultIndex] = right[rightIndex];
                    rightIndex++;
                }
            }
            return result;
            // combine two array into one in sorted order
        
        }

        public void SortInPlace(T[] array, int left, int right) //inplace
        {
            if (left >= right) //this is the break condition for the recursion
            {
                return;
            }
            int mid = left + (right - left) / 2;
            SortInPlace(array, left,mid);
            SortInPlace(array, mid+1 , right);
            MergeInPlace(array, left, mid, right);
        }
        private void MergeInPlace(T[] array, int left, int mid, int right)
        {
            int start2 = mid + 1;

            // If the direct merge is already sorted
            if (array[mid].CompareTo(array[start2]) <= 0)
            {
                return;
            }

            // Two pointers to maintain start of both arrays to merge
            while (left <= mid && start2 <= right)
            {
                // If element 1 is in right place
                if (array[left].CompareTo(array[start2]) <= 0)
                {
                    left++;
                }
                else
                {
                    T value = array[start2];
                    int index = start2;

                    // Shift all the elements between element 1 and element 2 right by 1
                    while (index != left)
                    {
                        array[index] = array[index - 1];
                        index--;
                    }
                    array[left] = value;

                    // Update all the pointers
                    left++;
                    mid++;
                    start2++;
                }
            }
        }
    }
}
