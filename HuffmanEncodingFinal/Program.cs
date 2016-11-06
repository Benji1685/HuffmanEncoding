using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace HuffmanEncodingFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Instantiation
            //Declaring our variables
            const int INPUT_FILE = 0;
            const int OUTPUT_FILE = 1;

            StreamReader reader = null;
            StreamWriter writer = null;
            string value = null;

            //we want to display an error if we are not able to read or write the input or output files
            if (args.Length != 2)
            {
                Console.WriteLine("Huffman Encoding");
                return;
            }
            //if the file does not exist then we display that along with the path we are searching for
            if (!File.Exists(args[INPUT_FILE]))
            {
                Console.WriteLine("The input file '{0}' does not exist.", args[INPUT_FILE]);
                return;
            }
            //If we are unable to find the output path destination or directory we display that
            if (File.Exists(args[OUTPUT_FILE]))
            {
                File.Delete(args[OUTPUT_FILE]);
            }
            #endregion
            #region ReadFile
            Console.WriteLine("Please enter Encode or Decode to start.");
            string UserInput = Console.ReadLine();

            if (UserInput.ToLower() == "encode")
            {
                Console.WriteLine("Please Wait, the file is encoding, this may take a while...");
                //We now need to read the file in
                reader = new StreamReader(File.OpenRead(args[INPUT_FILE]));
                value = reader.ReadToEnd();
                string input = value.ToString();
                HTree HuffmanTrees = new HTree();
                HuffmanTrees.CalcFrequencies(input);
                HuffmanTrees.BuildHTree();

                // Build the Huffman tree

                reader.Close();
            #endregion
                // Encode


                writer = new StreamWriter(File.OpenWrite(args[OUTPUT_FILE]));
                HuffmanTrees.WriteFrequenciesToFile(writer);
                HuffmanTrees.Encode(input, writer);
                writer.Flush();
                //we then say that for every charFrequency object that is located in our LinkedList
                //we want to write that to the console window and to the text file

           
            }
            else if (UserInput.ToLower() == "decode")
            {//We now need to read the file in
                reader = new StreamReader(File.OpenRead(args[INPUT_FILE]));
                Console.WriteLine("Please Wait, the file is decoding, this may take a while...");

                HTree HuffmanTrees = new HTree();
                HuffmanTrees.ReadFrequenciesFromFile(reader);
                HuffmanTrees.BuildHTree();

                writer = new StreamWriter(File.OpenWrite(args[OUTPUT_FILE]));
                // Decode
                HuffmanTrees.Decode(reader, writer);
                writer.Flush();
                //We then close the streamwriter
                writer.Close();
            }


            #region WriteFile





            //Creates a new instance of the object streamwriter and tells it to 
            //write to the output file

            //We then want to open the output file when we are done 
            //writing to it
            Process.Start(args[OUTPUT_FILE]);
            //we then print out the size of the list in the console window.
            #endregion

        }
    }
}