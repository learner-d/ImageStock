using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageStock.Utils
{
    public static class IEnumerableExt
    {
        public static void Swap<T>(ref T x, ref T y)
        {
            var temp = x;
            x = y;
            y = temp;
        }
        public static T[] RandomShuffle<T>(this IEnumerable<T> source)
        {
            var result = source.ToArray();
            if (result.Length < 2) return result;
            var random = new Random();
            for (int i = result.Length - 1; i > 0; i--)
            {
                int pos = random.Next(i + 1);
                if (pos != i) Swap(ref result[pos], ref result[i]);
            }
            return result;
        } 
    }
}