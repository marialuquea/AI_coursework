using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai4_maria
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Cave> caves = new List<Cave>();
            int caveID = 1;
            
            //Read input file
            string text = System.IO.File.ReadAllText(@"C:\Users\Maria\Documents\2nd year\Artificial Intelligence\try2\caverns files\input2.cav");
            Console.WriteLine("Raw data: " + text);

            //convert data to an array
            String[] data = text.Split(',');
            Console.WriteLine(data[0] + ' ' + data[1]);

            //extract data from the array
            int noOfCaves = Int16.Parse(data[0]);
            Console.WriteLine("There are " + noOfCaves + " caves.");

            //Get coordinates and build caves
            for (int count = 1; count < ((noOfCaves * 2) + 1); count = count + 2)
            {
                Console.WriteLine("Cave at " + data[count] + ","+ data[count + 1]);
                int x = Int16.Parse(data[count]);
                int y = Int16.Parse(data[count + 1]);

                Cave cave = new Cave(x, y, caveID);
                caves.Add(cave);

                caveID++;
            }

            //print all caves
            Console.WriteLine("caves list:");
            foreach(Cave c in caves)
            {
                Console.WriteLine(c.toString());
            }
            
            //Build connectivity matrix
            //Declare the array

            Boolean[][] connected = new Boolean[noOfCaves][];

            for(int _row = 0; _row < noOfCaves; _row++)
            {
                connected[_row] = new Boolean[noOfCaves];
            }
            //Now read data, starting point in array is after the coordinates
            int col = 0; int row = 0;
            for (int point = (noOfCaves*2)+1; point < data.Length; point++)
            {
                //work through the array

                if (data[point].Equals("1"))
                    connected[row][col] = true;
                else
                    connected[row][col] = false;

                row++;
                if (row == noOfCaves)
                {
                    row = 0;
                    col++;
                }
            }

            //find if caves are connected with caveIDs
            int cave1 = 11;
            int cave2 = 12;

            Boolean areConnected = connected[cave1 - 1][cave2 - 1];
            Console.WriteLine("Cave " + cave1 + " and cave " + cave2 + " are connected: " + areConnected);


            //Generate toCavesList
            for (int i = 1; i < caves.Count() + 1; i++)
            {
                Cave tempCave = null;
                Cave possibleChild = null;

                foreach(Cave c in caves)
                {
                    if (c.CaveID == i)
                    {
                        tempCave = c;
                        Console.WriteLine("tempCave: " + tempCave.toString());
                    }
                }

                for(int j = 1; j < caves.Count(); j++)
                {
                    foreach (Cave d in caves)
                    {
                        if (d.CaveID == j)
                        {
                            possibleChild = d;
                            //Console.WriteLine("possible child: " + possibleChild.toString());
                        }
                    }

                    int cave1ID = tempCave.CaveID;
                    int cave2ID = possibleChild.CaveID;
                    //Console.WriteLine("set caves ids");

                    Boolean check = connected[cave1ID - 1][cave2ID - 1];
                    Console.WriteLine("check: " + check);

                    if (!tempCave.Equals(possibleChild) && check)
                    {
                        tempCave.addToCavesList(possibleChild);
                        Console.WriteLine("possible child added");
                    }
                }


                //print caves to list
                foreach(Cave a in tempCave.ToCavesList)
                {
                    Console.WriteLine("Cave " + tempCave.CaveID + " can go to caves: " + a.CaveID);
                }

            }

            





            Console.ReadKey();

        }
    }
}
