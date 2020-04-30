using System;
using System.Text;

namespace GameOfLife.ConsoleUI
{
    class ConsoleRenderer : IRenderer
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        public ConsoleRenderer(int boardWidth, int boardHeight)
        {
            Console.SetWindowSize(boardWidth, boardHeight);
        }

        public void Draw(in CellState[,] cellState, int width, int height)
        {
            Console.Clear();
            stringBuilder.Clear();
            for (int y = height-1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    stringBuilder.Append(cellState[x, y] == CellState.Alive ? 'X' : '.');
                }
                // Add a new line unless we are on the last row
                if (y != 0)
                    stringBuilder.AppendLine();
            }
            Console.Write(stringBuilder.ToString());
        }
    }
}
