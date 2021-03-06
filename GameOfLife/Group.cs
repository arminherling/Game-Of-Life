﻿using System.Collections.Generic;
using System.Drawing;

namespace GameOfLife
{
    internal class Group
    {
        private readonly Point center;
        private readonly List<Point> neighbors;

        internal int X => center.X;
        internal int Y => center.Y;

        internal Group(Point center, List<Point> neighbors)
        {
            this.center = center;
            this.neighbors = neighbors;
        }

        internal CellState NextState(in CellState[,] cells)
        {
            int livingNeighbors = 0;
            foreach(var neighbor in neighbors)
            {
                if (cells[neighbor.X, neighbor.Y] == CellState.Alive)
                    livingNeighbors++;
            }

            if (cells[X, Y] == CellState.Alive)
            {
                if (livingNeighbors == 2 || livingNeighbors == 3)
                    return CellState.Alive;
                else
                    return CellState.Dead;
            }
            else
            {
                if (livingNeighbors == 3)
                    return CellState.Alive;
            }

            return cells[X, Y];
        }
    }
}
