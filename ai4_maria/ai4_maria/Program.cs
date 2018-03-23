using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ai4_maria
{
    /*
     * Author: Maria Luque Anguita 40280156
     * Description of class: main method, reads cave file and runs A* algorithm to find shortest path
     * Date last modified: 22/03/2018
     */
    class Program
    {
        public static void Main(string[] args)
        {
            while (true) {

                // arraylist that will store all caves
                ArrayList caves = new ArrayList();
                int caveID = 1;
                
                string mode = GetMode();

                // display error message
                if (mode != "s" && mode != "r")
                    Console.WriteLine(Environment.NewLine + "'r' or 's' please" + Environment.NewLine);

                // if mode chosen
                else
                {
                    if (mode == "s")
                    {
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("To step through the program press enter each time.");
                        Console.WriteLine("--------------------------------------------------");
                    }

                    string text = GetCavesFiles();

                    // if caves file is not empty
                    if (text != null)
                    {
                        //convert data to an array
                        String[] data = text.Split(',');

                        //extract data from the array
                        int noOfCaves = Int16.Parse(data[0]);

                        //Get coordinates and build caves
                        for (int count = 1; count < ((noOfCaves * 2) + 1); count = count + 2)
                        {
                            int x = Int16.Parse(data[count]);
                            int y = Int16.Parse(data[count + 1]);

                            Cave cave = new Cave(x, y, caveID);
                            caves.Add(cave);

                            caveID++;
                        }

                        //Build connectivity matrix
                        //Declare the array

                        Boolean[][] connected = new Boolean[noOfCaves][];

                        for (int _row = 0; _row < noOfCaves; _row++)
                        {
                            connected[_row] = new Boolean[noOfCaves];
                        }
                        //Now read data, starting point in array is after the coordinates
                        int col = 0; int row = 0;
                        for (int point = (noOfCaves * 2) + 1; point < data.Length; point++)
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

                        //Generate toCavesList
                        for (int i = 1; i < caves.Count + 1; i++)
                        {
                            Cave tempCave = null;
                            Cave possibleChild = null;

                            foreach (Cave c in caves)
                            {
                                if (c.CaveID == i)
                                {
                                    tempCave = c;
                                }
                            }

                            for (int j = 1; j < caves.Count + 1; j++)
                            {
                                foreach (Cave d in caves)
                                {
                                    if (d.CaveID == j)
                                    {
                                        possibleChild = d;
                                    }
                                }

                                int cave1ID = tempCave.CaveID;
                                int cave2ID = possibleChild.CaveID;

                                Boolean check = connected[cave1ID - 1][cave2ID - 1];

                                if (!tempCave.Equals(possibleChild) && check)
                                {
                                    tempCave.addToCavesList(possibleChild);
                                }
                            }
                        }

                        //Call A* function
                        Cave startCave = (Cave)caves[0];
                        Cave endCave = (Cave)caves[caves.Count - 1];

                        ArrayList aStarCaves = AstarSearch(startCave, endCave, mode);
                    }
                    else
                    {
                        Console.ReadKey();
                    }
                }
            }
        }

        //A star algorithm
        static ArrayList AstarSearch(Cave startCave, Cave endCave, String mode)
        {
            Cave tempCave;
            Boolean step = false;

            if (mode == "s")
            {
                step = true;
            }

            // make an open list containing only the starting node
            MethodsAStar open = new MethodsAStar();
            open.addCave(startCave);

            // show contents of open list if step by step program
            if (step)
            {
                Console.WriteLine("Open list: ");
                Console.WriteLine(" " + startCave.CaveID);
                Console.WriteLine("");
                Console.ReadKey();
            }

            // make an empty closed list
            MethodsAStar closed = new MethodsAStar();

            // show contents of closed list
            if (step)
            {
                Console.WriteLine("Closed list:");
                Console.WriteLine("");
                Console.ReadKey();
            }
            
            //while there are caves in the open list
            while (open.count() != 0)
            {
                //consider cave with smallest g in the open list
                tempCave = open.take_smallest();
                if (step)
                {
                    Console.WriteLine("---" + Environment.NewLine + "Current cavern: " + tempCave.CaveID +" removed from open list."+ Environment.NewLine + "---");
                    Console.WriteLine("Adding current cavern "+ tempCave.CaveID+" to closed list.");
                }

                //put temp cave in the closed list
                closed.addCave(tempCave);

                //display lists 
                if (step)
                {
                    Console.WriteLine("Open list: ");
                    List<int> openNow = open.printCaves();
                    foreach (int i in openNow)
                    {
                        Console.WriteLine(" " + i);
                    }
                    Console.WriteLine("");
                    Console.ReadKey();
                    Console.WriteLine("Closed list: ");
                    List<int> closedNow = closed.printCaves();
                    foreach (int i in closedNow)
                    {
                        Console.WriteLine(" " + i);
                    }
                    Console.WriteLine("");
                    Console.ReadKey();

                }

                //look at all the caves temp Cave can go to
                ArrayList to_caves_list = tempCave.ToCavesList;

                if (step)
                    Console.WriteLine("Look at the caves " + tempCave.CaveID + " can go to.");

                //for each to-cave of the tempCave
                foreach (Cave to_cave in to_caves_list)
                {
                    //while the cave is not in closed list and not in the open list
                    while ((closed.FindCave(to_cave.CaveID) == null) && (open.FindCave(to_cave.CaveID) == null))
                    {
                        if(step)
                            Console.WriteLine("Current cave beeing considered to go from  "+ tempCave.CaveID+ ": " + to_cave.CaveID);

                        // set its g
                        to_cave.g = tempCave.g + to_cave.eucledian(tempCave);
                        if(step)
                            Console.WriteLine("Current cave's total distance so far: " + to_cave.g + " units");

                        //change the to_cave's parent to this tempCave
                        to_cave.FromCave = tempCave;

                        //add it to the open list
                        open.addCave(to_cave);

                        //display lists
                        if (step)
                        {
                            Console.WriteLine("Open list: ");
                            List<int> openNow = open.printCaves();
                            foreach (int i in openNow)
                            {
                                Console.WriteLine(" " + i);
                            }
                            Console.WriteLine("");
                            Console.ReadKey();
                            Console.WriteLine("Closed list: ");
                            List<int> closedNow = closed.printCaves();
                            foreach (int i in closedNow)
                            {
                                Console.WriteLine(" " + i);
                            }
                            Console.WriteLine("");
                            Console.ReadKey();
                        }
                    }
                    //if to_cave has lower g value than temp
                    if (to_cave.g > (tempCave.g + to_cave.eucledian(tempCave)))
                    {
                        if (step)
                            Console.WriteLine("---" + Environment.NewLine + "Current cave beeing considered: " + to_cave.CaveID + Environment.NewLine + "---");

                        //replace the to_cave with the new, lower, g value
                        to_cave.g = tempCave.g + to_cave.eucledian(tempCave);
                        if (step)
                            Console.WriteLine("Current cave's total distance so far: " + to_cave.g + " units");

                        //tempCave is now the to_cave's parent
                        to_cave.FromCave = tempCave;

                        if (step)
                            Console.WriteLine(to_cave.CaveID + " is now officially part of the solution path");
                    }
                    
                }

                // if this cave is our destination cave
                if (tempCave == endCave)
                {
                    // Find the final path by looking at the FromCave feature
                    Cave current = endCave;

                    //create a list of caves to print to the solution
                    ArrayList answer = new ArrayList();

                    while (current != null)
                    {
                        //add from endCave to startCave
                        answer.Add(current);
                        current = current.FromCave;
                    }

                    answer.Reverse();

                    //Print solution path
                    Console.WriteLine(Environment.NewLine + Environment.NewLine + "----------------------------");
                    Console.WriteLine("     SOLUTION PATH: ");
                    foreach (Cave c in answer)
                    {
                        Console.WriteLine("        " + c.CaveID);
                    }
                    Console.WriteLine(Environment.NewLine + "Total distance: " + String.Format("{0:0.00}", endCave.g) + " units"); 
                    Console.WriteLine("----------------------------" + Environment.NewLine + Environment.NewLine); 
                    
                }

            }

            return null;
        }
        
        // Read mode (run once or run step by step)
        static string GetMode()
        {
            Console.WriteLine("Press 'r' to run the program quickly or 's' to run it step by step: ");
            string mode = Console.ReadLine();
            return mode;
        }
        
        //Read file that user types
        static string GetCavesFiles()
        {
            Console.Write("Name of file you want to check (include extension): ");
            string input = Console.ReadLine();

            try
            {
                // find current directory
                string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                // go to directory where the files are saved
                string caverns_path = Path.GetFullPath(Path.Combine(path, @"..\..\..\")) + input;
                // read text on file
                string files = System.IO.File.ReadAllText(caverns_path);
                return files;
            }
            catch (FileNotFoundException)
            {
                // if file not found
                Console.WriteLine("File not found... Press enter to try again");
                return null;
            }
            
        }
        
    }
}