using System.Collections;

namespace ai4_maria
{
    class MethodsAStar
    {

        ArrayList _listOfCaves;
        CaveComparer _caveComparer;
        
        public MethodsAStar()
        {
            _listOfCaves = new ArrayList();
            _caveComparer = new CaveComparer();
        }

        // return a cave found by its ID or null if not found
        public Cave FindCave(int index)
        {
            foreach(Cave c in _listOfCaves)
            {
                if (c.CaveID == index)
                {
                    return c;
                }
            }
            return null;
        }

        // add cave and sort list
        public void addCave(Cave c)
        {
            int k = _listOfCaves.BinarySearch(c, _caveComparer);

            if (k == -1) // no element
            {
                _listOfCaves.Insert(0, c);
            }
            else if (k < 0) // find location by complement
            {
                k = ~k;
                _listOfCaves.Insert(k, c);
            }
            else if (k >= 0)
            {
                _listOfCaves.Insert(k, c);
            }

        }

        // return the Cave with the smallest g value from the list
        // and remove it from the list
        public Cave take_smallest()
        {
            Cave r = (Cave)_listOfCaves[0];
            _listOfCaves.RemoveAt(0);
            return r;
        }

        //return count of items in the list
        public int count()
        {
            return _listOfCaves.Count;
        }
        
    }
    
}
//hii
