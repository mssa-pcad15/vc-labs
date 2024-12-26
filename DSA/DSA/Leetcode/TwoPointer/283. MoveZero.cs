using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Leetcode.TwoPointer
{
    public class MoveZerosLeet
    {
        public void MoveZeroes(int[] nums)
        {
            List<int> l = new();
            l.AddRange(nums.Where(n => n != 0).Concat(nums.Where(n => n == 0)));
            l.CopyTo(nums);
        }

        public void MoveZeroes2(int[] nums)
        {

            int pointerLeft = 0;
           
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != 0)
                {
                    nums[pointerLeft++] = nums[i];
                }
            }
            for (int j = pointerLeft; j < nums.Length; j++)
            {
                nums[j] = 0;
            }

        }
    }
}
