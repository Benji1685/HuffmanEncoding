using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanEncodingFinal
{
    class HNode : IComparable<HNode>
    {
        public HNode LeftNode { get; set; }
        public HNode RightNode { get; set; }
        public int CharFrequency { get; set; }
        public char Character { get; set; }

        public override string ToString()
        {
            return CharFrequency.ToString();
        }

        public int CompareTo(HNode node)
        {
            return this.CharFrequency.CompareTo(node.CharFrequency);
        }
        public List<bool> TraverseTree(char character, List<bool> data)
        {
            if (!isBranch())
            {
                if (character == this.Character)
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
                List<bool> left = data.ToArray().ToList();
                List<bool> right = data.ToArray().ToList();



                left.Add(true);
                right.Add(false);

                return (RightNode.TraverseTree(character, right) ?? LeftNode.TraverseTree(character, left));
            }
        }

        public bool isBranch()
        {
            return LeftNode != null && RightNode != null;

        }
    }
}

