using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public class GameBoard
    {
        private readonly int width;
        private readonly int height;
        private Stack<CellState[,]> history = new Stack<CellState[,]>();

        public GameBoard(int boardWidth, int boardHeight)
        {
            width = boardWidth;
            height = boardHeight;
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

        public void Update()
        {
        }

        public void Draw(IRenderer renderer)
        {
            renderer.Draw(history.Peek(), width, height);
        }
    }
}
