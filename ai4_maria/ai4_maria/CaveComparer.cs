using System;
using System.Collections;


namespace ai4_maria
{
    public class CaveComparer:IComparer
    {

        public CaveComparer() { }

        public int Compare(object x, object y)
        {
            return (int)((Cave)x).g - (int)((Cave)y).g;
        }

    }
}
