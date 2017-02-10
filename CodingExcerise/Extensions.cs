using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

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
            //todo: what do brackets do in regex's?
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

        /// <summary>
        /// Remove one item from the Enumerable whose value matches valueToExclude
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">IEnumerable of T to opperate on</param>
        /// <param name="valueToExclude">a value to exclude from the IEnumerable of T</param>
        /// <returns></returns>
        public static IEnumerable<T> ExceptOne<T>(this IEnumerable<T> source, T valueToExclude)
        {
            //todo: why the ToList?
            return source.ToList().Tee( t=>t.Remove(valueToExclude) ).ToList();
        }

        private static T Tee<T>(this T @this, Action<T> action)
        {
            action(@this);
            return @this;
        }
    }
}