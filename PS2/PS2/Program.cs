using System;
using System.Collections.Generic;

namespace PS2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read in first line to get array specs
            string line = Console.ReadLine();
            
            // Initialize number of prototypes and number of layers in each
            int numPrototypes = 0;
            int numLayers = 0;
            Int32.TryParse(line[0] + "", out numPrototypes);
            Int32.TryParse(line[2] + "", out numLayers);

            // Create arrays to represent the prototypes in different ways 
            string[] lines = new string[numPrototypes];
            Node[] protos = new Node[numPrototypes];
            string[] protoReps = new string[numPrototypes];

            // Populate the raw string input array
            for (int j = 0; j < numPrototypes; j++)
            {
                string curr = Console.ReadLine();
                lines[j] = curr;
            }

            // Create BSTs and populate the node array - based on raw string array
            CreateBSTs(lines, protos, numLayers);

            // Iterate through BSTs and create string representation - based node array     
            for (int i = 0; i < protos.Length; i++)
            {
                Node n = protos[i];
                List<string> rep = new List<string>();
                rep = CreateBstReps(n, rep);
                protoReps[i] = String.Join("", rep);
            }

            // Add each string representation to a set, then find final answer
            HashSet<string> set = new HashSet<string>();
            foreach (string s in protoReps)
            {
                set.Add(s);
            }

            Console.WriteLine(set.Count);
        }

        // Recursively traverse each prototype/BST in pre-order (start at root)
        public static List<String> CreateBstReps(Node prototype, List<string> currProto)
        {
            if (prototype.left != null)
            {
                CreateBstReps(prototype.left, currProto);
                // We've returned from a left-dead-end
                currProto.Add("L");
            }

            // We've hit the current node in ascending order
            currProto.Add("X");
            
            if (prototype.right != null)
            {
                CreateBstReps(prototype.right, currProto);
                // We've returned from a right-dead-end
                currProto.Add("R");
            }

            return currProto;
        }

        public static void CreateBSTs(string[] lines, Node[] protos, int numLayers)
        {
            // Iterate through array of input lines
            for (int l = 0; l < lines.Length; l++)
            {
                string line = lines[l] + " ";
                string numCat = "";
                int[] numArr = new int[numLayers];
                int numArrCount = 0;
                // Create a number array to avoid space issues
                for (int i = 0; i < line.Length; i++)
                {
                    string currChar = line[i] + "";
                    if (string.IsNullOrWhiteSpace(currChar))
                    {
                        int num = 0;
                        Int32.TryParse(numCat, out num);
                        numArr[numArrCount] = num;
                        numCat = "";
                        numArrCount++;
                    }
                    else
                    {
                        numCat += line[i] + "";
                    }
                }

                // Iterate through number array
                for (int i = 0; i < numArr.Length; i++)
                {
                    // Create new node
                    int val = numArr[i];
                    Node curr = new Node(val);

                    // Add node to prototypes list     
                    if (protos[l] == null)
                    {
                        protos[l] = curr;                      
                    }
                    else
                    {
                        // Find where to insert node and assign parent connection
                        Node root = protos[l];
                        Node parent = findInsertionSpot(val, root);
                        curr.parent = parent;

                        // If parent is larger, put on left
                        if (parent.value > val)
                        {
                            parent.left = curr;
                        }
                        // Otherwise, put on right
                        else
                        {
                            parent.right = curr;
                        }
                    }
                }
            }
        }

        // Recursively finds parent node
        private static Node findInsertionSpot(int val, Node node)
        {
            if (node.value > val && node.left != null)
            {
                return findInsertionSpot(val, node.left);
            }
            else if (node.value < val && node.right != null)
            {
                return findInsertionSpot(val, node.right);
            }
            else
            {
                return node;
            }
        }
    }

    // Represents individual BST node
    public class Node
    {
        public int value;
        public Node left;
        public Node right;
        public Node parent;

        public Node(int val)
        {
            value = val;
            left = null;
            right = null;
            parent = null;
        }
    }
}
