using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Leetcode.TwoPointer
{
    public class SubsequenceOfLeet
    {
        public bool IsSubsequence(string s, string t)
        {
            if (s.Length == 0) return true;
            if (t.Length == 0) return false;
            int sPointer = 0, tPointer = 0;

            while (sPointer < s.Length)
            {

                while (tPointer < t.Length)
                {
                    if (t[tPointer] == s[sPointer])
                    {
                        sPointer++;
                        if (sPointer == s.Length) return true;
                    }

                    tPointer++;
                    if (tPointer == t.Length) return false;
                }
            }
            return true;
        }

        public bool IsSubsequence2(string s, string t)
        {
            return s.IsSubsequenceOf(t);  // is s subsequence of t;
        }
    }

    public static class StringExt
    {

        public static bool IsSubsequenceOf(this string source, string target)
        {

            int targetPointer = 0;
            int matches = 0;
            foreach (char c in source)
            {
                for (; targetPointer < target.Length;)
                {
                    if (target[targetPointer] == c)
                    {
                        matches++;
                        if (source.Length == matches) return true;
                        break;
                    }
                    targetPointer++;
                }
                targetPointer++;
            }
           
            return false;
        }
    }
}
