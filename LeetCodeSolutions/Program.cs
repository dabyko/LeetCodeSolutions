using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

namespace LeetCodeSolutions
{
    internal class Program
    {
        /*  1. Two Sum

            Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.
            You may assume that each input would have exactly one solution, and you may not use the same element twice.
            You can return the answer in any order.

            Example 1:
            Input: nums = [2,7,11,15], target = 9
            Output: [0,1]
            Explanation: Because nums[0] + nums[1] == 9, we return [0, 1].
        */

        static int[] TwoSum(int[] nums, int target)
        {
            var myDict = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int res = target - nums[i];

                if (myDict.ContainsKey(res))
                    return new int[] { myDict[res], i };

                if (!myDict.ContainsKey(res))
                    myDict.Add(nums[i], i);
            }

            throw new Exception("No Two Sum !!!");
        }

        static void Main(string[] args)
        {
            /* Two Sum */
            int[] mass = TwoSum(new int[] { 1, 2, 4, 5, 6 }, 7);
            Console.WriteLine("Mass:[" + mass[0] + "," + mass[1] + "]");

        }
    }
}