using System;
using System.Collections;

namespace ai4_maria
{
    /*
     * Author: Maria Luque Anguita 40280156
     * Description of class: compare g values of caves to sort them with the smallest g first
     * Date last modified: 15/03/2018
     */
    public class CaveComparer:IComparer
    {
        //constructor
        public CaveComparer() { }

        //main  method, compares 2 objects by one of their values
        public int Compare(object x, object y)
        {
            return (int)((Cave)x).g - (int)((Cave)y).g;
        }
    }
}
