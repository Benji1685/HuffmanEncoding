using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanEncodingFinal
{
    public class Node
    {
        public char Character { get; set; }
        public int Frequency { get; set; }
        public Node Right { get; set; }
        public Node Left { get; set; }
 
        public List<bool> Traverse(char charctr,List<bool> data)
        {
            // Leaf
            if (Right == null && Left == null)
            {
                if (charctr.Equals(this.Character))
                {
                    return data;
                }   
                else
                {
                    return null;
                }
            }
            else
            {
                List<bool> left = null;
                List<bool> right = null;
 
                if (Left != null)
                {
                    //if the the left of the tree node contains a value then we are going to create a bool and return 0 (false)
                    List<bool> leftPath = new List<bool>();
                    leftPath.AddRange(data);
                    leftPath.Add(false);
 
                    left = Left.Traverse(charctr, leftPath);
                }
 
                if (Right != null)
                {
                    // if the right of the tree node is not null then we are going to create a list of bools and return 1 (true)
                    List<bool> rightPath = new List<bool>();
                    rightPath.AddRange(data);
                    rightPath.Add(true);
                    right = Right.Traverse(charctr, rightPath);
                }
 
                if (left != null)
                {
                    return left;
                }
                else
                {
                    return right;
                }
            }
        }
    }
}
