using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCAofBT
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var root = new TreeNode(3);
            root.left = new TreeNode(5);
            root.right = new TreeNode(1);
            root.left.left = new TreeNode(6);
            root.left.right = new TreeNode(2);
            root.left.right.left = new TreeNode(7);
            root.left.right.right = new TreeNode(4);

            root.right.left = new TreeNode(0);
            root.right.right = new TreeNode(8);
            Console.WriteLine(root);
            //var bfSolution = new SolutionBruteForce(root);
            //var stack = bfSolution.GetTreePath(7);
            var optimalSolution = new OplimalSolution(root);
            var node = optimalSolution.LowestCommonAncestor(root, new TreeNode(6), new TreeNode(2));

        }
    }

    public class SolutionBruteForce
    {
        public TreeNode treeNode;

        public SolutionBruteForce(TreeNode treeNode) { this.treeNode = treeNode; }
        public List<int> GetTreePath(int val)
        {
            List<int> nodes = new List<int>();
            Traverse(treeNode, val, nodes);
            return nodes;
        }

        public bool Traverse(TreeNode treeNode, int val, List<int> nodes)
        {
            if (treeNode == null) return false;
            nodes.Add(treeNode.val);
            if (treeNode.val == val) return true;
            var isOnLeft = Traverse(treeNode.left, val, nodes);
            var inOnRight = Traverse(treeNode.right, val, nodes);
            if (isOnLeft == false && inOnRight == false)
            {
                nodes.RemoveAt(nodes.Count - 1);
                return false;
            }
            else return true;
               


        }
    }

    public class OplimalSolution
    {
        public TreeNode treeNode;

        public OplimalSolution (TreeNode treeNode) { this.treeNode = treeNode; }

        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null)
            {
                return null;
            }
            if (root.val == p.val || root.val == q.val)
            {
                return root;
            }
            var leftSideResolve = LowestCommonAncestor(root.left, p, q);
            var rightSideResolve = LowestCommonAncestor(root.right, p, q);
            if (leftSideResolve == null && rightSideResolve == null)
            {
                return null;
            }
            else if (leftSideResolve != null && rightSideResolve == null)
            {
                return leftSideResolve;
            }
            else if (leftSideResolve == null && rightSideResolve != null)
            {
                return rightSideResolve;
            }
            else 
            {
                return root;
            }

        }
    }

}
