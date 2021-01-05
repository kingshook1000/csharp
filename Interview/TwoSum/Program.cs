using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

//https://leetcode.com/problems/two-sum/
// idea is only two position matters. Not sequential from start to end index. Out out will be two indexes and if you add the two indexes in the array content it will be the target
// If not found such two indexes then throw exception
namespace TwoSum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = new int[] { 2, 7, 11, 15 };
            int target = 9;
            var solutionBruteForce = new SolutionBruteForce();
            var result = solutionBruteForce.TwoSum(nums, target);
            Console.WriteLine("Completed");

        }
    }

    public class SolutionBruteForce
    {
        public int[] TwoSum(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
                for (int j = i+1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        return new int[] { i, j };
                    }
                }
            throw new ArgumentException();
        }
    }

    public class SolutionLinear
    {
        public int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> complements = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                var complement = target - nums[i];
                if (complements.TryGetValue(complement, out int val))
                {
                    return new int[] { val, i };
                }
                else 
                {
                    complements.Add(nums[i], i);
                }
            }
                
            throw new ArgumentException();
        }
    }
}
