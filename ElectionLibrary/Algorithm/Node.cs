using System;
using ElectionLibrary.Environment;

namespace ElectionLibrary
{
    public class Node
    {
        public Position Pos { get; }

        public int HeuristicCost { get; set; }
        public int FinalCost { get; set; }

        public Node Parent { get; set; }

	    public Node(int x, int y) {
            Pos = new Position(x, y);
	        this.HeuristicCost = -1;
            this.FinalCost = -1;
	        this.Parent = null;
	    }

        public override string ToString() {
            return "(" + Pos.X + "," + Pos.Y + ")";
        }
    }
}
