﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameOfLife
{
    public class Cells
    {
        private readonly CellState[,] cells;
        private readonly List<Group> groups;

        public int Width { get; }
        public int Height { get; }

        public Cells(int arrayWidth, int arrayHeight)
        {
            Width = arrayWidth;
            Height = arrayHeight;
            cells = new CellState[Width, Height];
            groups = new List<Group>();
            GenerateGroups();
            Randomize();
        }

        internal Cells(int arrayWidth, int arrayHeight, in CellState[,] newCells, List<Group> newGroups)
        {
            Width = arrayWidth;
            Height = arrayHeight;
            cells = newCells;
            groups = newGroups;
        }

        internal void Clear()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    cells[x, y] = CellState.Dead;
                }
            }
        }
        private void Randomize()
        {
            var random = new Random();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    cells[x, y] = random.Next(0, 2) == 0
                        ? CellState.Dead
                        : CellState.Alive;
                }
            }
        }

        private void GenerateGroups()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var yAbove = y == Height - 1 ? 0 : y + 1;
                    var yBelow = y == 0 ? Height - 1 : y - 1;
                    var xLeft = x == 0 ? Width - 1 : x - 1;
                    var xRight = x == Width - 1 ? 0 : x + 1;

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
            var newCells = new CellState[Width, Height];
            foreach(var group in groups)
            {
                newCells[group.X, group.Y] = group.NextState(cells);
            }
            return new Cells(Width, Height, newCells, groups);
        }

        public void Draw(IRenderer renderer)
        {
            renderer.Draw(cells, Width, Height);
        }
    }
}
