using System;
namespace ElectionSimulator
{
    public class Node
    {
        private int x;

	    private int y;

	    private int heuristicCost;

	    private int finalCost;

	    private Node parent;

	    public Node(int x, int y) {
	        this.x = x;
	        this.y = y;
	        this.heuristicCost = -1;
	        this.finalCost = -1;
	        this.parent = null;
	    }

	    public int GetX() {
	        return x;
	    }

	    public void SetX(int x) {
	        this.x = x;
	    }

	    public int GetY() {
	        return y;
	    }

	    public void SetY(int y) {
	        this.y = y;
	    }

	    public int GetHeuristicCost() {
	        return heuristicCost;
	    }

	    public void SetHeuristicCost(int heuristicCost) {
	        this.heuristicCost = heuristicCost;
	    }

	    public int GetFinalCost() {
	        return finalCost;
	    }

	    public void SetFinalCost(int finalCost) {
	        this.finalCost = finalCost;
	    }

	    public Node GetParent() {
	        return parent;
	    }

	    public void SetParent(Node parent) {
	        this.parent = parent;
	    }

        public override string ToString() {
            return "(" + x + "," + y + ")";
        }
    }
}
