using System.Collections;

namespace ai4_maria
{
    class MethodsAStar
    {

        ArrayList list;
        CaveComparer _caveComparer;
        

        public MethodsAStar()
        {
            list = new ArrayList();
            _caveComparer = new CaveComparer();
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

        public void addCave(Cave c)
        {
            
            int k = list.BinarySearch(c, _caveComparer);

            if (k == -1) // no element
            {
                list.Insert(0, c);
            }
            else if (k < 0) // find location by complement
            {
                k = ~k;
                list.Insert(k, c);
            }
            else if (k >= 0)
            {
                list.Insert(k, c);
            }

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
        
    }
    
}
