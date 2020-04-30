using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfLife
{
    public class Cells
    {
        private readonly int width;
        private readonly int height;
        private readonly CellState[,] cells;
        private readonly List<Group> groups;

        public Cells(int arrayWidth, int arrayHeight)
        {
            width = arrayWidth;
            height = arrayHeight;
            cells = new CellState[width, height];
            groups = new List<Group>();
            GenerateGroups();
            Randomize();
        }

        internal Cells(int arrayWidth, int arrayHeight, in CellState[,] newCells, List<Group> newGroups)
        {
            width = arrayWidth;
            height = arrayHeight;
            cells = newCells;
            groups = newGroups;
        }

        private void Randomize()
        {
            var random = new Random();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    cells[x, y] = random.Next(0, 2) == 0
                        ? CellState.Dead
                        : CellState.Alive;
                }
            }
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

        public Cells Next()
        {
            var newCells = new CellState[width, height];
            foreach(var group in groups)
            {
                newCells[group.X, group.Y] = group.NextState(cells);
            }
            return new Cells(width, height, newCells, groups);
        }

        public void Draw(IRenderer renderer)
        {
            renderer.Draw(cells, width, height);
        }
    }
}
