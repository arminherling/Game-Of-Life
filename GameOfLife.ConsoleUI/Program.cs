using System;
using System.Threading.Tasks;

namespace GameOfLife.ConsoleUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const int boardWidth = 80;
            const int boardHeight = 60;
            var renderer = new ConsoleRenderer(boardWidth, boardHeight);
            var board = new GameBoard(boardWidth, boardHeight);
            while (true)
            {
                board.Update();

                renderer.Clear();
                board.Draw(renderer);

                await Task.Delay(TimeSpan.FromSeconds(0.5));
            }
        }
    }
}
