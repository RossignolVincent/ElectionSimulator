using System;
using System.Collections.Generic;
using ElectionLibrary.Environment;

namespace ElectionLibrary.Algorithm
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

        public AStar(Position start, Position target, int[,] cells)
		{
			this.cells = ConvertArray(cells);
            this.start = this.cells[start.Y, start.X];
			this.target = this.cells[target.Y, target.X];
			this.isComputed = false;

			InitLists();
			ComputeHeuristics();
			DisplayCells(null);
		}

		public AStar(Position start, Position target, Node[,] cells)
		{
			this.cells = cells;
            this.start = this.cells[start.Y, start.X];
			this.target = this.cells[target.Y, target.X];
			this.isComputed = false;

			InitLists();
			ComputeHeuristics();
		}

		public AStar(Position start, Position target, List<List<AbstractArea>> cells)
		{
            this.cells = ConvertArray(cells);
			this.start = this.cells[start.Y, start.X];
			this.target = this.cells[target.Y, target.X];
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

        public static Node[,] ConvertArray(List<List<AbstractArea>> inputs)
        {
            Node[,] outputs = new Node[inputs.Count, inputs[0].Count];

            for (int y = 0; y < inputs.Count; y++)
            {
                for (int x = 0; x < inputs[0].Count; x++)
                {
                    if(inputs[y][x] is Street)
                    {
                        outputs[y, x] = new Node(x, y);
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
                        cells[y, x].HeuristicCost = Math.Abs(start.Pos.X - y) + Math.Abs(target.Pos.Y - x);
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

			int newFinalCost = neighbour.HeuristicCost + cost;
			bool inOpen = openNodes.Contains(neighbour);

			if (!inOpen || (neighbour.FinalCost > 0 && neighbour.FinalCost > newFinalCost))
			{
				neighbour.FinalCost = newFinalCost;
				neighbour.Parent = current;

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
				if (current.Pos.X > 0)
				{
					UpdateNeighbour(current, cells[current.Pos.Y, current.Pos.X - 1], current.FinalCost + V_H_COST);
				}

				// right
                if (current.Pos.X + 1 < cells.GetLength(0))
				{
                    UpdateNeighbour(current, cells[current.Pos.Y, current.Pos.X + 1], current.FinalCost + V_H_COST);
				}

				// top
				if (current.Pos.Y > 0)
				{
                    UpdateNeighbour(current, cells[current.Pos.Y - 1, current.Pos.X], current.FinalCost + V_H_COST);
				}

				// bottom
                if (current.Pos.Y + 1 < cells.GetLength(1))
				{
                    UpdateNeighbour(current, cells[current.Pos.Y + 1, current.Pos.X], current.FinalCost + V_H_COST);
				}
			}

			isComputed = true;
			return current;
		}

        private Node GetMinCostOpenNode()
        {
            Node result = null;

            foreach(Node node in openNodes) {
                if (result == null || result.FinalCost > node.FinalCost) {
                    result = node;
                }
            }

            return result;
        }

        public List<Node> GetResult()
		{
			List<Node> result = null;
			if (target.Parent != null)
			{
				result = new List<Node>();
				result.Add(target);
				Node current = target.Parent;
				while (current != null)
				{
					result.Add(current);
					current = current.Parent;
				}
			}

			return result;
		}

        public Stack<Position> GetPath()
        {
            Stack<Position> result = null;
			if (target.Parent != null)
			{
                result = new Stack<Position>();
                result.Push(target.Pos);
				Node current = target.Parent;
				while (current != null)
				{
                    result.Push(current.Pos);
					current = current.Parent;
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
