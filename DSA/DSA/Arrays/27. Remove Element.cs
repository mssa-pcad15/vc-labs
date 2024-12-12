using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Arrays
{
    public class LeetCode27
    {
        public static int RemoveElement(int[] input, int val) {
            
            int endIndex = input.Length - 1;
            int countElement=0;
            for (int i = 0; i < input.Length; i++)
            {
                if (endIndex == i) { break; }
                countElement++;
            
                if (input[i] == val)
                {
                    (input[i],input[endIndex])=(input[endIndex], input[i]);
                    endIndex--;
                }
            }
            return countElement;

        }
    }
}
