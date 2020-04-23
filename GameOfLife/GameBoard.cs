using System;

namespace GameOfLife
{
    public class GameBoard
    {
        private readonly int width;
        private readonly int height;

        public GameBoard(int boardWidth, int boardHeight)
        {
            width = boardWidth;
            height = boardHeight;
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Draw(IRenderer renderer)
        {
            throw new NotImplementedException();
        }
    }
}
