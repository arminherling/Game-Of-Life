using System;
using System.Threading.Tasks;

namespace GameOfLife.ConsoleUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const int boardWidth = 140;
            const int boardHeight = 70;
            var life = new GameOfLife(boardWidth, boardHeight);
            var renderer = new ConsoleRenderer(boardWidth, boardHeight);
            while (true)
            {
                life.Draw(renderer);
                await Task.Delay(TimeSpan.FromSeconds(0.15));
                life.Update();
            }
        }
    }
}
