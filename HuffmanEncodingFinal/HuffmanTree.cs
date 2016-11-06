using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanEncodingFinal
{
    public class HuffmanTree
    {
        private List<Node> TreeNode = new List<Node>();
        public Node RootNode { get; set; }
        public Dictionary<char, int> CharFrequencies = new Dictionary<char, int>();

        public void Build(string source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (!CharFrequencies.ContainsKey(source[i]))
                {
                    CharFrequencies.Add(source[i], 0);
                }

                CharFrequencies[source[i]]++;
            }

            foreach (KeyValuePair<char, int> charctr in CharFrequencies)
            {
                TreeNode.Add(new Node() { Character = charctr.Key, Frequency = charctr.Value });
            }

            while (TreeNode.Count > 1)
            {
                List<Node> orderedTreeNode = TreeNode.OrderBy(node => node.Frequency).ToList<Node>();

                if (orderedTreeNode.Count >= 2)
                {
                    // Take first two items
                    List<Node> taken = orderedTreeNode.Take(2).ToList<Node>();

                    // Create a parent node by combining the CharFrequencies
                    Node parent = new Node()
                    {
                        Character = '*',
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    TreeNode.Remove(taken[0]);
                    TreeNode.Remove(taken[1]);
                    TreeNode.Add(parent);
                }

                this.RootNode = TreeNode.FirstOrDefault();

            }

        }

        public BitArray Encode(string source)
        {
            List<bool> encodedSource = new List<bool>();

            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = this.RootNode.Traverse(source[i], new List<bool>());
                encodedSource.AddRange(encodedSymbol);
            }

            BitArray bits = new BitArray(encodedSource.ToArray());

            return bits;
        }

        public string Decode(BitArray bits)
        {
            Node current = this.RootNode;
            string decoded = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (IsLeaf(current))
                {
                    decoded += current.Character;
                    current = this.RootNode;
                }
            }

            return decoded;
        }

        public bool IsLeaf(Node node)
        {
            return (node.Left == null && node.Right == null);
        }
    }
}
