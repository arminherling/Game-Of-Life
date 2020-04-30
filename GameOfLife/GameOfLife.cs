using System.Collections.Generic;

namespace GameOfLife
{
    public class GameOfLife
    {
        private Stack<Cells> history = new Stack<Cells>();

        public int Generation => history.Count;
        public GameOfLife(int boardWidth, int boardHeight)
        {
            history.Push(new Cells(boardWidth, boardHeight));
        }

        public void Draw(IRenderer renderer)
        {
            var currentCells = history.Peek();
            currentCells.Draw(renderer);
        }

        public void Update()
        {
            var currentCells = history.Peek();
            var nextState = currentCells.Next();
            history.Push(nextState);
        }
    }
}
