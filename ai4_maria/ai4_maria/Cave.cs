using System;
using System.Collections;

namespace ai4_maria
{
    /*
     * Author: Maria Luque Anguita 40280156
     * Description of class: defines Cave object (with properties and methods)
     * Date last modified: 15/03/2018
     */
    class Cave
    {
        //properties of cave objects
        private int _x;
        private int _y;
        private int _caveID;
        private Cave _fromCave;
        private ArrayList _toCavesList = new ArrayList();
        private double _g;

        //constructor
        public Cave(int x, int y, int caveID)
        {
            _x = x;
            _y = y;
            _caveID = caveID;
        }

        // CaveID getter and setter
        public int CaveID
        {
            get { return _caveID; }
            set { _caveID = value; }
        }

        // G value
        public double g
        {
            get { return _g; }
            set { _g = value; }
        }
        
        // Cave it comes from
        public Cave FromCave
        {
            set { _fromCave = value; }
            get { return _fromCave; }
        }
        
        // add cave to caves it can go to
        public void addToCavesList(Cave c)
        {
            this._toCavesList.Add(c);
        }

        // return list of caves it can go to
        public ArrayList ToCavesList
        {
            get { return _toCavesList; }
        }

        // Calculate distance between 2 caves
        public double eucledian(Cave c)
        {
            return (Math.Sqrt(Math.Pow(this._x - c._x, 2) + Math.Pow(this._y - c._y, 2)));
        }
 
    }
}
