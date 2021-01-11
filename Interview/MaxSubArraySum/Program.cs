using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSubArraySum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[] { -2, -1, 1, 2, -4, 4, -3, 10 };
            var solution = new Solution();
            int sum = solution.GetMaxSubArraySum(numbers);
        }
    }

    public class Solution
    {
        public int GetMaxSubArraySum(int[] nums)
        {
            int i = 0;
            int max = -10000;
            int runningSum = 0;
            while (nums[i] < 0)
            {
                if (nums[i] > max) 
                {
                    max = nums[i];
                }
                i++;
            }
            for (; i < nums.Length; i++ )
            {
                if ((runningSum + nums[i]) > 0)
                {
                    runningSum += nums[i];
                }
                else {
                    runningSum = 0;
                }
                if (runningSum > max)
                    max = runningSum;
            }
            return max;


        }
    }
}
