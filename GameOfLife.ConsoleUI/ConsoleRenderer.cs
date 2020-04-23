using System;

namespace GameOfLife.ConsoleUI
{
    class ConsoleRenderer : IRenderer
    {
        public ConsoleRenderer(int boardWidth, int boardHeight)
        {
            Console.SetWindowSize(boardWidth, boardHeight);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
