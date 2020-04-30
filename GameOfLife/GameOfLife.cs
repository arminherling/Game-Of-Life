using System.Collections.Generic;

namespace GameOfLife
{
    public class GameOfLife
    {
        private List<Cells> history = new List<Cells>();

        public int Generations => history.Count;
        public int CurrentGeneration { get; private set; } = 0;
        public GameOfLife(int boardWidth, int boardHeight)
        {
            history.Add(new Cells(boardWidth, boardHeight));
        }

        public void Draw(IRenderer renderer)
        {
            var currentCells = history[CurrentGeneration];
            currentCells.Draw(renderer);
        }

        public void StepForward()
        {
            if (CurrentGeneration == Generations - 1)
            {
                var currentCells = history[CurrentGeneration];
                var nextState = currentCells.Next();
                history.Add(nextState);
            }
            CurrentGeneration++;
        }

        public void StepBackward()
        {
            if (CurrentGeneration != 0)
                CurrentGeneration--;
        }
    }
}
