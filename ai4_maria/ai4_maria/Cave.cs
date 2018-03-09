using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private List<Cave> _toCavesList = new List<Cave>();

        public Cave(int x, int y, int caveID)
        {
            _x = x;
            _y = y;
            _caveID = caveID;
        }
        // x, y and caveID
        public int getX
        {
            get { return _x; }
            set { _x = value; }
        }

        public int getY
        {
            get { return _y; }
            set { _y = value; }
        }

        public int CaveID
        {
            get { return _caveID; }
            set { _caveID = value; }
        }

        // G value
        public double getG_value
        {
            get { return _g; }
        }

        public double calculateGCostTo(Cave c)
        {
            double distance = System.Math.Sqrt((this._x * c._x) + (this._y * c._y));
            return (c._g + distance);
        }

        public void setGCostTo(Cave c)
        {
            double distance = System.Math.Sqrt((this._x * c._x) + (this._y * c._y));
            this._g = (c._g + distance);
        }

        // H value
        public void setHeuristicCostToGoal(Cave c)
        {
            double distance = System.Math.Sqrt((this._x * c._x) + (this._y * c._y));
            this._h = distance;
        }

        // Total cost
        public double getTotalCost()
        {
            return (_g + _h);
        }

        // From cave
        public Cave FromCave
        {
            set { _fromCave = value; }
            get { return _fromCave; }
        }

        // To caves
        public void addToCavesList(Cave c)
        {
            this._toCavesList.Add(c);
        }

        public List<Cave> ToCavesList
        {
            get { return _toCavesList; }
        }

        // ToString
        public string toString()
        {
            return "Cave { x = " + _x + 
                ", y = " + _y + 
                ", caveID = " + _caveID + "}";
        }

        // Equals
        public Boolean Equals(Cave c)
        {
            if (c == null)
            {
                return false;
            }

            if (this._x == c._x && this._y == c._y && this._caveID == c._caveID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
