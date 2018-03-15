using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace ai4_maria
{
    class Program
    {

        public static void Main(string[] args)
        {

            Boolean flag = true;

            while (flag == true) {

                ArrayList caves = new ArrayList();
                int caveID = 1;

                //Read input file
                string text = GetCavesFiles();

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

                    ArrayList aStarCaves = AstarSearch(startCave, endCave);
                }
                else
                {
                    Console.ReadKey();
                }
            }
        }

        //A star algorithm
        static ArrayList AstarSearch(Cave startCave, Cave endCave)
        {
            MethodsAStar open = new MethodsAStar();

            open.addCave(startCave);

            MethodsAStar closed = new MethodsAStar();
            

            while(open.count() != 0)
            {
                Cave tempCave = open.take_smallest();

                // if the last node is reached
                if (tempCave == endCave)
                {
                    // Find the final path and print
                    Cave current = endCave;

                    ArrayList answer = new ArrayList();

                    while (current != null)
                    {
                        answer.Add(current);

                        current = current.FromCave;
                    }

                    answer.Reverse();

                    foreach (Cave c in answer)
                    {
                        Console.WriteLine("cave in solution path: " + c.CaveID);
                    }

                    return answer;
                }

                closed.addCave(tempCave);

                ArrayList to_caves_list = tempCave.ToCavesList;
                
                foreach (Cave to_cave in to_caves_list)
                {
                    if ((closed.FindCave(to_cave.CaveID) == null))
                    {
                        if ((open.FindCave(to_cave.CaveID) == null))
                        {

                            to_cave.g = tempCave.g + to_cave.eucledian(tempCave);

                            to_cave.FromCave = tempCave;

                            open.addCave(to_cave);
                            
                        }
                        else
                        {
                            if (to_cave.g > ((tempCave.g) + to_cave.eucledian(tempCave)))
                            {
                                to_cave.g = tempCave.g + to_cave.eucledian(tempCave);

                                to_cave.FromCave = tempCave;
                            }
                        }
                    }

                    
                }

            }
            return null;
        }

        //Read file that user types
        static string GetCavesFiles()
        {
            string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            Console.Write("Name of file you want to check (include extension): ");

            string input = Console.ReadLine();
            
            try
            {
                string caverns_path = Path.GetFullPath(Path.Combine(path, @"..\..\..\")) + input;

                string files = System.IO.File.ReadAllText(caverns_path);
                
                return files;
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("File not found... Press enter to try again");

                return null;
            }
        }
        
    }
    //15 march 2018
}