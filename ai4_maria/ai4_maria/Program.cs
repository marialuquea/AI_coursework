using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai4_maria
{
    class Program
    {
        public static void Main(string[] args)
        {
            ArrayList caves = new ArrayList();
            int caveID = 1;
            
            //Read input file
            string text = System.IO.File.ReadAllText(@"C:\Users\Maria\Documents\2nd year\Artificial Intelligence\try2\caverns files\input1.cav");
            //Console.WriteLine("Raw data: " + text);

            //convert data to an array
            String[] data = text.Split(',');

            //extract data from the array
            int noOfCaves = Int16.Parse(data[0]);
            //Console.WriteLine("There are " + noOfCaves + " caves.");

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
            /*Console.WriteLine("caves list:");
            foreach(Cave c in caves)
            {
                Console.WriteLine(c.toString());
            }
            */
            
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
            //int cave1 = 1;
            //int cave2 = 2;

            //Boolean areConnected = connected[cave1 - 1][cave2 - 1];
            //Console.WriteLine("Cave " + cave1 + " and cave " + cave2 + " are connected: " + areConnected);


            //Generate toCavesList
            for (int i = 1; i < caves.Count + 1; i++)
            {
                Cave tempCave = null;
                Cave possibleChild = null;

                foreach(Cave c in caves)
                {
                    if (c.CaveID == i)
                    {
                        tempCave = c;
                        //Console.WriteLine("tempCave: " + tempCave.toString());
                    }
                }

                for(int j = 1; j < caves.Count + 1; j++)
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
                    //Console.WriteLine("parent "+cave1ID+", possible child: "+cave2ID);

                    Boolean check = connected[cave1ID - 1][cave2ID - 1];
                    //Console.WriteLine("check: " + check);

                    if (!tempCave.Equals(possibleChild) && check)
                    {
                        tempCave.addToCavesList(possibleChild);
                        //Console.WriteLine("tempCave: "+ tempCave.CaveID + ", possibleChild: "+possibleChild.CaveID);
                    }
                }


                //print caves to list
                /*
                foreach(Cave a in tempCave.ToCavesList)
                {
                    Console.WriteLine("Cave " + tempCave.CaveID + " can go to caves: " + a.CaveID);
                }
                */
            }




            
            //Call A* function
            Cave startCave = (Cave)caves[0];
           // Console.WriteLine("startCave: "+startCave.CaveID);
            Cave endCave = (Cave)caves[caves.Count - 1];
            //Console.WriteLine("endCave: "+endCave.CaveID);

            ArrayList aStarCaves = AstarSearch(startCave, endCave);


            Console.ReadKey();

        }

        static ArrayList AstarSearch(Cave startCave, Cave endCave)
        {
            /*
            MethodsAStar open = new MethodsAStar();
            MethodsAStar closed = new MethodsAStar();

            open.addCave(startCave);

            Cave currentCave = startCave;

            while (currentCave != endCave)
            {

            }
            */

            //  DARWONS
            MethodsAStar open = new MethodsAStar();
            MethodsAStar closed = new MethodsAStar();
            
            open.addCave(startCave);

            while(open.size() > 0)
            {
                Cave currentCave = open.pop();

                //Console.WriteLine("YOOOOOO TEST");

                if (currentCave.Equals(endCave))
                {
                //    Console.WriteLine("i got here");
                    return printSolutionPath(endCave);
                }

                closed.addCave(currentCave);
                //Console.WriteLine("closed.size = " + closed.size());

                //print open and closed lists
                /*
                Console.WriteLine("Open list:");
                foreach (Cave c in open.listOfCaves)
                {
                    Console.WriteLine(c.CaveID);
                }
                Console.WriteLine("Closed list:");
                foreach (Cave c in closed.listOfCaves)
                {
                    Console.WriteLine(c.CaveID);
                }
                */

                ArrayList children = currentCave.ToCavesList;

                foreach(Cave e in children)
                {

                //    Console.WriteLine( "YOOOOO   " + e.toString());
                }

                foreach (Cave currentChild in children)
                {

                    if ((closed.FindCave(currentChild.CaveID) == null))
                    {
                       // Console.WriteLine("currentChild: "+currentChild.CaveID);
                        //if currentCave is not in the open list
                        if ((open.FindCave(currentChild.CaveID)== null))
                        {
                            currentChild.FromCave = currentCave;
                            currentChild.setHeuristicCostToGoal(endCave);
                            currentChild.setGCostTo(currentCave);
                            //Console.WriteLine("currentChild: " + currentChild.CaveID + ", G value: " + currentChild.G_value+ ", H value: "+currentChild.hValue);


                            open.addCave(currentChild);
                        }
                        else
                        {
                            if (currentChild.G_value > currentChild.calculateGCostTo(currentCave))
                            {
                                currentChild.FromCave = currentCave;
                                currentChild.setGCostTo(currentCave);
                            }
                        }
                    }

                    
                }

            }
            return null;
            
        }
        
        static ArrayList printSolutionPath(Cave endCave)
        {
            Cave solutionCave = endCave;
            ArrayList solutionPath = new ArrayList();

            while (solutionCave != null)
            {
               // Console.WriteLine(solutionCave + " ONLy one ");
                solutionPath.Add(solutionCave);
                solutionCave = solutionCave.FromCave;
                
            }

            solutionPath.Reverse();

            foreach(Cave c in solutionPath)
            {
                Console.WriteLine("cave in solution path: "+c.CaveID);
            }

            return solutionPath;
        }
    }
}

//friday 9 march