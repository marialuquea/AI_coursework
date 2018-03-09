using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai4_maria
{
    class MethodsAStar
    {
        //SortedCaveList

        List<Cave> list;

        public MethodsAStar()
        {
            list = new List<Cave>();
        }

        public List<Cave> getList
        {
            get { return list; }
        }

        public Cave findCave(int index)
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
            Cave c = this.findCave(index);
            if (c != null)
            {
                list.Remove(c);
            }
        }

        public void addCave(Cave c)
        {
            if (list.Count == 0)
            {
                list.Add(c);
            }
            else
            {
                list.Add(c);

                list.Sort((x, y) => x.getTotalCost().CompareTo(y.getTotalCost()));
            }
        }
        
        public Cave pop()
        {
            Cave c = list[0];
            list.Remove(list[0]);
            return c;
        }

    }
}
