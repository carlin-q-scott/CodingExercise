using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodingExcerise
{
    /// <summary>
    /// Just some extensions to help things be more readable and serve generic purposes
    /// </summary>
    public static class Extensions
    {

        public static void Dump(this object obj)
        {
            Console.WriteLine(obj);
        }

        /// <summary>
        /// Partition a <paramref name="source"/> into groups of <paramref name="partitionSize"/> size
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="partitionSize"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int partitionSize)
        {
            return source
                .Select((val, index) => new { Index = index, Value = val })
                .GroupBy(o => o.Index / partitionSize)
                .Select(o => o.Select(v => v.Value).ToList())
                .ToList();
        }

        /// <summary>
        /// Generic Array of T value swap: swaps array[i] &lt;=&gt; array[j]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public static void Swap<T>(this T[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        public static string RemoveAllWhiteSpace(this string str)
        {
            return Regex.Replace(str, @"[\s]+", String.Empty);
        }

        /// <summary>
        /// Get everything in an IEnumerable&lt;T&gt; except the <paramref name="valueToExclude"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="valueToExclude"></param>
        /// <returns></returns>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, T valueToExclude)
        {
            return source.Except(new[] {valueToExclude});
        }
    }
}