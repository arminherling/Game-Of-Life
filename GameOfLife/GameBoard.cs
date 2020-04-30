using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfLife
{
    public class GameBoard
    {
        private readonly int width;
        private readonly int height;
        private Stack<CellState[,]> history = new Stack<CellState[,]>();
        private List<Group> groups = new List<Group>();

        public GameBoard(int boardWidth, int boardHeight)
        {
            width = boardWidth;
            height = boardHeight;
            GenerateGroups();
            PushRandomizedCells();
        }

        private void PushRandomizedCells()
        {
            var random = new Random();
            var cells = new CellState[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    cells[x, y] = random.Next(0, 2) == 0
                        ? CellState.Dead
                        : CellState.Alive;
                }
            }
            history.Push(cells);
        }

        private void GenerateGroups()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var yAbove = y == height - 1 ? 0 : y + 1;
                    var yBelow = y == 0 ? height - 1 : y - 1;
                    var xLeft = x == 0 ? width - 1 : x - 1;
                    var xRight = x == width - 1 ? 0 : x + 1;

                    var neighbors = new List<Point>();
                    neighbors.Add(new Point(xLeft, yAbove));
                    neighbors.Add(new Point(x, yAbove));
                    neighbors.Add(new Point(xRight, yAbove));

                    neighbors.Add(new Point(xLeft, y));
                    var center = new Point(x, y);
                    neighbors.Add(new Point(xRight, y));

                    neighbors.Add(new Point(xLeft, yBelow));
                    neighbors.Add(new Point(x, yBelow));
                    neighbors.Add(new Point(xRight, yBelow));

                    groups.Add(new Group(center, neighbors));
                }
            }
        }

        public void Update()
        {
            var currentState = history.Peek();
            var newState = new CellState[width, height];

            foreach(var group in groups)
            {
                newState[group.X, group.Y] = group.NextState(currentState);
            }
            history.Push(newState);
        }

        public void Draw(IRenderer renderer)
        {
            renderer.Draw(history.Peek(), width, height);
        }
    }
}
