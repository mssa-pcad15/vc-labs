using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSATest
{
    internal static class AssertExtensions
    {
        public static void ArrayAreEqualInContent(this Assert a,Array left, Array right) { 
            
            if (left.Rank!= right.Rank)
                 { Assert.Fail("Arrays are of different dimension."); }
            
            for (int i = 0; i < left.Rank; i++)
            {
                if (left.GetLength(i) != right.GetLength(i)) {
                    Assert.Fail($"Array contains different number of elements at rank {i}.");
                }


                for (int j = 0; j < left.GetLength(left.Rank-1); j++)
                {
                   
                    if (!left.GetValue(i, j)!.Equals(right.GetValue(i, j)))
                    {
                        Assert.Fail($"Array contains different elements at index {i},{j}.");
                    }
                }
            }

           
        }
    }
}
