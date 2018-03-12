using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai4_maria
{
    class MethodsAStar
    {
        //SortedCaveList

        ArrayList list;
        CaveComparer _caveComparer;
        

        public MethodsAStar()
        {
            list = new ArrayList();
            _caveComparer = new CaveComparer();
        }

        public ArrayList getList
        {
            get { return list; }
        }

        public Cave FindCave(int index)
        {


            foreach(Cave c in list)
            {
                if (c.CaveID == index)
                {
                    return c;
                }
            }
            return null;
        }

        public void removeCave(int index)
        {
            Cave c = this.FindCave(index);
            if (c != null)
            {
                list.Remove(c);
            }
        }

        public void addCave(Cave c)
        {
            
            int k = list.BinarySearch(c, _caveComparer);

            if (k == -1) // no element
                list.Insert(0, c);
            else if (k < 0) // find location by complement
            {
                k = ~k;
                list.Insert(k, c);
            }
            else if (k >= 0)
                list.Insert(k, c);
            


            /*
            //if list doesn't contain Cave c
            while (list.Contains(c) == false)
            {
                list.Add(c);
                list = orderByTotalCost(list);
            }
            
            if (list.Count == 0)
            {
                list.Add(c);
            }
            else
            {
                list.Add(c);
                list.Sort((x, y) => x.getTotalCost().CompareTo(y.getTotalCost()));
            }
            */


        }

        public Cave pop()
        {
            Cave r = (Cave)list[0];
            list.RemoveAt(0);
            return r;
        }

        public int size()
        {
            return list.Count;
        }

        public ArrayList listOfCaves
        {
            get { return list; }
        }

        //NOT USED
        public double compare(Cave x, Cave y)
        {
            return x.getTotalCost() - y.getTotalCost();
        }


        /*
        //USED
        public  ArrayList orderByTotalCost(ArrayList cavesList)
        {
            ArrayList orderedList = new ArrayList();

            var totalCosts = from cave in cavesList
                             orderby cave.getTotalCost()
                             select cave;

            foreach(Cave c in totalCosts)
            {
                orderedList.Add(c);
                Console.WriteLine(c.CaveID + " " + c.G_value);
            }

            foreach(Cave e in orderedList)
            {
           //     Console.WriteLine(e.toString());
            }
            Console.WriteLine("-----------");
            return orderedList;
            
        }

        public int Compare(object x, object y)
        {
            throw new NotImplementedException();
        }

    */
    }

    //friday 9 march
}
