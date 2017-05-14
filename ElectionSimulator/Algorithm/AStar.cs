using System;
using System.Collections.Generic;

namespace ElectionSimulator
{
    public class AStar
    {
        private const int DIAGONAL_COST = 14;
		private const int V_H_COST = 10;

        private List<Node> openNodes;
		private List<Node> closedNodes;

		private Node[,] cells;

		private Node start;
		private Node target;

        private bool isComputed;

		public AStar(int startX, int startY, int targetX, int targetY, int[,] cells)
		{
			this.cells = ConvertArray(cells);
			this.start = this.cells[startY, startX];
			this.target = this.cells[targetY, targetX];
			this.isComputed = false;

			InitLists();
			ComputeHeuristics();
			DisplayCells(null);
		}

		public AStar(int startX, int startY, int targetX, int targetY, Node[,] cells)
		{
			this.cells = cells;
			this.start = this.cells[startY, startX];
			this.target = this.cells[targetY, targetX];
			this.isComputed = false;

			InitLists();
			ComputeHeuristics();
		}

		public static Node[,] ConvertArray(int[,] inputs)
		{
            Node[,] outputs = new Node[inputs.GetLength(0), inputs.GetLength(1)];

            for (int y = 0; y < inputs.GetLength(0); y++)
			{
                for (int x = 0; x < inputs.GetLength(1); x++)
				{
					if (inputs[y, x] == 0)
					{
                        outputs[y ,x] = new Node(x, y);
					}
					else
					{
						outputs[y, x] = null;
					}
				}
			}

			return outputs;
		}

		private void InitLists()
		{
            this.openNodes = new List<Node>();
            this.closedNodes = new List<Node>();
		}

		private void ComputeHeuristics()
		{
            for (int y = 0; y < cells.GetLength(0); y++)
			{
                for (int x = 0; x < cells.GetLength(1); x++)
				{
					if (cells[y, x] != null)
					{
                        cells[y, x].SetHeuristicCost(Math.Abs(start.GetX() - y) + Math.Abs(target.GetY() - x));
					}
				}
			}
		}

		private void UpdateNeighbour(Node current, Node neighbour, int cost)
		{
			if (neighbour == null || closedNodes.Contains(neighbour))
			{
				return;
			}

			int newFinalCost = neighbour.GetHeuristicCost() + cost;
			bool inOpen = openNodes.Contains(neighbour);

			if (!inOpen || (neighbour.GetFinalCost() > 0 && neighbour.GetFinalCost() > newFinalCost))
			{
				neighbour.SetFinalCost(newFinalCost);
				neighbour.SetParent(current);

				if (!inOpen)
				{
					openNodes.Add(neighbour);
				}
			}
		}

		public Node Compute()
		{
			openNodes.Add(start);
			Node current;

			while (true)
			{
                current = GetMinCostOpenNode();
                openNodes.Remove(current);

				if (current == null || current.Equals(target))
				{
					break;
				}
				closedNodes.Add(current);

				// Compute neighbours
				// left
				if (current.GetX() > 0)
				{
					UpdateNeighbour(current, cells[current.GetY(), current.GetX() - 1], current.GetFinalCost() + V_H_COST);
				}

				// right
                if (current.GetX() + 1 < cells.GetLength(0))
				{
					UpdateNeighbour(current, cells[current.GetY(), current.GetX() + 1], current.GetFinalCost() + V_H_COST);
				}

				// top
				if (current.GetY() > 0)
				{
					UpdateNeighbour(current, cells[current.GetY() - 1, current.GetX()], current.GetFinalCost() + V_H_COST);
				}

				// bottom
                if (current.GetY() + 1 < cells.GetLength(1))
				{
					UpdateNeighbour(current, cells[current.GetY() + 1, current.GetX()], current.GetFinalCost() + V_H_COST);
				}
			}

			isComputed = true;
			return current;
		}

        private Node GetMinCostOpenNode()
        {
            Node result = null;

            foreach(Node node in openNodes) {
                if (result == null || result.GetFinalCost() > node.GetFinalCost()) {
                    result = node;
                }
            }

            return result;
        }

        private List<Node> GetResult()
		{
			List<Node> result = null;
			if (target.GetParent() != null)
			{
				result = new List<Node>();
				result.Add(target);
				Node current = target.GetParent();
				while (current != null)
				{
					result.Add(current);
					current = current.GetParent();
				}
			}

			return result;
		}

		private void DisplayCells(List<Node> result)
		{
            for (int y = 0; y < cells.GetLength(0); y++)
			{
                for (int x = 0; x < cells.GetLength(1); x++)
				{
					Node cell = cells[y, x];
					string str;

					if (cell == null)
					{
						str = "W";
					}
					else if (cell == start)
					{
						str = "S";
					}
					else if (cell == target)
					{
						str = "T";
					}
					else if (result != null && result.Contains(cell))
					{
						str = "X";
					}
					else
					{
						str = "0";
					}
                    Console.Out.Write(str + "  ");
				}
				Console.Out.WriteLine("");
			}
			Console.Out.WriteLine("------------------");
		}

		public void DisplayResult()
        {
			if (!isComputed)
			{
				Compute();
			}
			List<Node> result = GetResult();

            if (result == null || result.Count == 0)
			{
				Console.Out.WriteLine("No path found...");
			}
			else
			{
				DisplayCells(result);
			}
		}
    }
}
