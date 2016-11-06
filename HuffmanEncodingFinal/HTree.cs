using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HuffmanEncodingFinal
{
    class HTree
    {
        private List<HNode> HuffmanNodes = new List<HNode>();
        private HNode RootNode { get; set; }
        public int[] Frequencies = new int[256];


        public void CalcFrequencies(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                Frequencies[(byte)input[i]]++;
            }
        }
        public void WriteFrequenciesToFile(StreamWriter wr)
        {
            BinaryWriter br = new BinaryWriter(wr.BaseStream);

            for (int i = 0; i < Frequencies.Length; i++)
            {
                br.Write(Frequencies[i]);
            }

        }
        public void ReadFrequenciesFromFile(StreamReader rf)
        {
            BinaryReader br = new BinaryReader(rf.BaseStream);

            for (int i = 0; i < Frequencies.Length; i++)
            {
                Frequencies[i] = br.ReadInt32();
            }
        }
        public void BuildHTree()
        {
            // Counts the Frequencies
          

            //creating the Array of Huffman Leafs
            for (int i = 0; i < 256; i++)
            {
                if (Frequencies[i] != 0)
                {
                    HuffmanNodes.Add(new HNode()
                    {
                        Character = (char)i,
                        CharFrequency = Frequencies[i]
                    });
                }
            }
            //Actally building the tree and taking the smallest frequencies and adding them together
            //then back into the tree
            while (HuffmanNodes.Count > 1)
            {
                HuffmanNodes.Sort();

                HNode first = HuffmanNodes[0];
                HNode second = HuffmanNodes[1];

                HuffmanNodes.Remove(first);
                HuffmanNodes.Remove(second);

                HNode Branch = new HNode();
                Branch.Character = '\0';
                Branch.CharFrequency = first.CharFrequency + second.CharFrequency;
                Branch.LeftNode = first;
                Branch.RightNode = second;
                HuffmanNodes.Add(Branch);
            }
            this.RootNode = HuffmanNodes[0];
        }

        // encode the input
        public void Encode(string input, TextWriter write)
        {
            

            for (int i = 0; i < input.Length; i++)
            {
                //calls the tree traversal
                List<bool> listchar = this.RootNode.TraverseTree(input[i], new List<bool>());

                for (int j = 0; j < listchar.Count; j++ )
                {
                    //add to the bool array
                    if (listchar[j])
                    {
                        write.Write("1");
                    }
                    else
                    {
                        write.Write("0");
                    }
                }
            }
         
            
        }
        //Decoding the file
        public void Decode(StreamReader input, StreamWriter output)
        {
            HNode CurrentNode = this.RootNode;
            

          while (!input.EndOfStream)
            {
                bool b = input.Read() == '1';
                    CurrentNode = b ?
                        CurrentNode.LeftNode : CurrentNode.RightNode;

                    if (!CurrentNode.isBranch())
                    {
                        output.Write(CurrentNode.Character);
                        CurrentNode = this.RootNode;
                    }
            }
        }
    }
}
