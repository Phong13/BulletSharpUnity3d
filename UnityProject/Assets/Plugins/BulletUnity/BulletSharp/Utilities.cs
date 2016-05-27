/*
* Copyright (c) 2007-2010 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace BulletSharp
{
    static class Utilities
    {
        /// <summary>
        /// The value for which all absolute numbers smaller than are considered equal to zero.
        /// </summary>
        public const float ZeroTolerance = 1e-6f;

        /// <summary>
        /// Compares two floating point numbers based on an epsilon zero tolerance.
        /// </summary>
        /// <param name="left">The first number to compare.</param>
        /// <param name="right">The second number to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is within epsilon of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool EpsilonEquals(float left, float right)
        {
            return System.Math.Abs(left - right) <= ZeroTolerance;
        }

        /// <summary>
        /// Compares two floating point numbers based on an epsilon zero tolerance.
        /// </summary>
        /// <param name="left">The first number to compare.</param>
        /// <param name="right">The second number to compare.</param>
        /// <param name="epsilon">The epsilon value to use for zero tolerance.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is within epsilon of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool EpsilonEquals(float left, float right, float epsilon)
        {
            return System.Math.Abs(left - right) <= epsilon;
        }

        /// <summary>
        /// Swaps two items.
        /// </summary>
        /// <typeparam name="T">The type of the items to swap.</typeparam>
        /// <param name="left">The first item to swap.</param>
        /// <param name="right">The second item to swap.</param>
        public static void Swap<T>(ref T left, ref T right)
        {
            T temp = left;
            left = right;
            right = temp;
        }

        /// <summary>
        /// Does something with arrays.
        /// </summary>
        /// <typeparam name="T">Most likely the type of elements in the array.</typeparam>
        /// <param name="value">Who knows what this is for.</param>
        /// <param name="count">Probably the length of the array.</param>
        /// <returns>An array of who knows what.</returns>
        public static T[] Array<T>(T value, int count)
        {
            T[] result = new T[count];
            for (int i = 0; i < count; i++)
                result[i] = value;

            return result;
        }
    }
}
