using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

namespace LeetCodeSolutions
{

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;         
        }
    }

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

        /*2. Add Two Numbers
           You are given two non-empty linked lists representing two non-negative integers.The digits are stored in reverse order, 
           and each of their nodes contains a single digit.Add the two numbers and return the sum as a linked list.
           You may assume the two numbers do not contain any leading zero, except the number 0 itself.
            Example 2:
            Input: l1 = [2,4,3], l2 = [5,6,4]
            Output: [7,0,8]
            Explanation: 342 + 465 = 807.
        */

        static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode result = new ListNode();
            ListNode head = result;

            int carry = 0;

            while (l1 != null || l2 != null)
            {
                int val_1 = (l1 != null) ? l1.val : 0;
                int val_2 = (l2 != null) ? l2.val : 0;

                int sum = val_1 + val_2 + carry;

                carry = sum / 10;

                int digitToWrite = sum % 10;

                head.next = new ListNode(digitToWrite);

                if (l1 != null) l1 = l1.next;
                if (l2 != null) l2 = l2.next;

                head = head.next;

            }

            if (carry > 0)
            {
                ListNode node = new ListNode(carry);
                head.next = node;
            }

            return result.next;
        }

        /*9. Palindrome Number
        Given an integer x, return true if x is a palindrome, and false otherwise.*/

        static bool IsPalindrome(int x)
        {
            if (x == 0)
                return true;

            if (x < 0)
                return false;

            if (x % 10 == 0)
                return false;

            int digit_rev = 0;

            while (x > digit_rev)
            {
                int pop = x % 10;

                digit_rev = (digit_rev * 10) + pop;

                x /= 10;
            }

            return (x == digit_rev || x == digit_rev / 10) ? true : false;

        }

        static void ShowList(ListNode l)
        {
            ListNode newList = l;

            for (int i = 0; newList != null; i++)
            {
                Console.WriteLine("Element [" + i + "]" + " = " + newList.val + "\n");
                newList = newList.next;
            }
        }

        /*13. Roman to Integer*/

        static int RomanToInt(string s)
        {
            var romans = new Dictionary<char, int>(){
                {'I', 1},
                {'V', 5},
                {'X', 10},
                {'L', 50},
                {'C', 100},
                {'D', 500},
                {'M', 1000}
            };

            int res = 0, prevNumb = 0;

            for (int i = 0; i < s.Length; i++)
            {
                int curNumb = romans[s[i]];

                res = curNumb <= prevNumb ? res + curNumb : res + (curNumb - (2 * prevNumb));

                prevNumb = curNumb;
            }

            return res;
        }

        /*14. Longest Common Prefix*/

        static string LongestCommonPrefix(string[] strs)
        {
            /*    
                if (strs.Length == 0) return "";

                string pref = strs[0];

                for(int i = 1; i < strs.Length; i++)
                {
                    while(strs[i].IndexOf(pref) != 0)
                    {
                        pref = pref.Substring(0, pref.Length - 1);
                    }
                }

                return pref;
            */


            if (strs.Length == 0) return "";

            string pref = "";

            for (int i = 0; i < strs[0].Length; i++)
            {
                for (int s = 0; s < strs.Length; s++)
                {
                    if (i == strs[s].Length || strs[0][i] != strs[s][i])
                    {
                        return pref;
                    }

                }

                pref += strs[0][i];
            }

            return pref;
        }

        static bool IsValid(string s)
        {
            if (s.Length == 0)
                return false;

            Stack<char> brackets = new Stack<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(' || s[i] == '[' || s[i] == '{')
                {
                    brackets.Push(s[i]);
                }
                else
                {
                    if (brackets.Count == 0)
                        return false;

                    if (s[i] == ')' && brackets.Pop() != '(')
                        return false;

                    if (s[i] == ']' && brackets.Pop() != '[')
                        return false;

                    if (s[i] == '}' && brackets.Pop() != '{')
                        return false;
                }


            }

            if (brackets.Count > 0)
                return false;


            return true;
        }

        static ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {

            if (list1 == null && list2 == null)
                return null;

            ListNode resNode = new ListNode();

            ListNode resPointer = resNode;

            ListNode pointerL1 = list1;
            ListNode pointerL2 = list2;

            while (pointerL1 != null && pointerL2 != null)
            {
                if (pointerL1.val < pointerL2.val)
                {
                    resPointer.next = new ListNode(pointerL1.val);
                    pointerL1 = pointerL1.next;
                }
                else
                {
                    resPointer.next = new ListNode(pointerL2.val);
                    pointerL2 = pointerL2.next;
                }

                resPointer = resPointer.next;
            }


            resPointer.next = pointerL1 != null ? pointerL1 : pointerL2; 

            return resNode.next;
        }

        static ListNode MergeTwoListsRecursive(ListNode list1, ListNode list2)
        {
            // Base case: if one of the lists is empty, return the other list
            if (list1 == null) return list2;
            if (list2 == null) return list1;

            ListNode result;

            // Select the smaller element in the current node
            if (list1.val < list2.val)
            {
                result = new ListNode(list1.val);
                result.next = MergeTwoListsRecursive(list1.next, list2);
            }
            else
            {
                result = new ListNode(list2.val);
                result.next = MergeTwoListsRecursive(list1, list2.next);
            }

            return result;
        }

        /*26. Remove Duplicates from Sorted Array*/
        static int RemoveDuplicates(int[] nums)
        {
           /* int k = 0;

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] != nums[k])
                {
                    nums[++k] = nums[i];
                }
            }
            return k + 1;*/
            if (nums.Length == 0)
                return 0;

            int currPos = 0;
            int checkPos = 1;


            while (checkPos < nums.Length)
            {
                if (nums[currPos] != nums[checkPos])
                {
                    currPos++;
                    nums[currPos] = nums[checkPos];
                }

                checkPos++;                     
            }


            return currPos + 1;
        }

        static void Main(string[] args)
        {
            /* Two Sum */
            int[] mass = TwoSum(new int[] { 1, 2, 4, 5, 6 }, 7);
            Console.WriteLine("Two sum \n");
            Console.WriteLine("Mass:[" + mass[0] + "," + mass[1] + "] \n");

            /* Add Two Numbers */ 
            ListNode node1_3 = new ListNode(3);
            ListNode node1_2 = new ListNode(4, node1_3);
            ListNode l1 = new ListNode(2, node1_2);

            ListNode node2_3 = new ListNode(4);
            ListNode node2_2 = new ListNode(6, node2_3);
            ListNode l2 = new ListNode(5, node2_2);

            ListNode res = AddTwoNumbers(l1, l2);

            Console.WriteLine("Add Two Numbers \n");
            ShowList(res);

            /* Palindrome Number */
            int x = 12321;
            Console.WriteLine("Palindrome Number \n");
            Console.WriteLine("Number: " + x + " Palindrome status: "+ IsPalindrome(x) + "\n");

            /* RomanToInt */
            Console.WriteLine("RomanToInt: " + RomanToInt("LVIII"));

            /*Longest Common Prefix*/

            string[] strs = new string[] { "flower", "flow", "flight" };

            Console.WriteLine("Longest Common Prefix: " + LongestCommonPrefix(strs));

            /*20.Valid Parentheses*/

            Console.WriteLine("Valid Parentheses: " + IsValid("(]"));

            /*21.Merge Two Sorted Lists*/

            ListNode subNode1_3 = new ListNode(4);
            ListNode subNode1_2 = new ListNode(2, subNode1_3);
            ListNode list1 = new ListNode(1, subNode1_2);

            ListNode subNode2_3 = new ListNode(4);
            ListNode subNode2_2 = new ListNode(3, subNode2_3);
            ListNode list2 = new ListNode(1, subNode2_2);

            ListNode mergeList = MergeTwoListsRecursive(list1, list2);

            Console.WriteLine("Merge Two Sorted Lists \n");
            ShowList(mergeList);

            /*26.Remove Duplicates from Sorted Array*/

            Console.WriteLine("Remove Duplicates from Sorted Array: " + RemoveDuplicates(new int[] {0,0,1,1,1,2,2,3,3,4}));
        }
    }
}