using System;
using System.Collections;

namespace ai4_maria
{
    class Cave
    {

        private int _x;
        private int _y;
        private int _caveID;

        private double _g;
        private double _h;

        private Cave _fromCave;
        private ArrayList _toCavesList = new ArrayList();

        public Cave(int x, int y, int caveID)
        {
            _x = x;
            _y = y;
            _caveID = caveID;
        }
        
        public int CaveID
        {
            get { return _caveID; }
            set { _caveID = value; }
        }
        
        public double g_value
        {
            get { return _g; }
        }

        public double calculateGCostTo(Cave c)
        {
            double distance = Math.Sqrt(Math.Pow(this._x - c._x, 2) + Math.Pow(this._y - c._y, 2));
            return (c._g + distance);
        }

        public void setGCostTo(Cave c)
        {
            double distance = Math.Sqrt(Math.Pow(this._x - c._x, 2) + Math.Pow(this._y - c._y, 2));
            this._g = (c._g + distance);
        }
        
        public void setHeuristicCostToGoal(Cave c)
        {
            double distance = Math.Sqrt(Math.Pow(this._x - c._x, 2) + Math.Pow(this._y - c._y, 2));
            this._h = distance;
        }
        
        public int getTotalCost()
        {
            return (int)(_g + _h);
        }
        
        public Cave FromCave
        {
            set { _fromCave = value; }
            get { return _fromCave; }
        }
        
        public void addToCavesList(Cave c)
        {
            this._toCavesList.Add(c);
        }

        public ArrayList ToCavesList
        {
            get { return _toCavesList; }
        }
        
        public string toString()
        {
            return "Cave { x = " + _x +
                ", y = " + _y +
                ", caveID = " + _caveID + "}";
        }
 
    }
}
