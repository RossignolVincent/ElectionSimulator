using System;

namespace ElectionSimulator
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TestAStar();
        }

        private static void TestAStar()
        {
            int[,] cells = new int[5, 5]{
                {0, 0, 0, 0, 1},
                {0, 0, 0, 0, 0},
                {0, 0, 1, 0, 0},
                {0, 1, 0, 1, 0},
                {0, 1, 0, 0, 0},
	        };

	        AStar aStar = new AStar(0, 0, 2, 3, cells);
	        Node target = aStar.Compute();

	        aStar.DisplayResult();
        }
    }
}
