using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public class GameOfLife
    {
        private List<Cells> history = new List<Cells>();

        public int Generations => history.Count;
        public int CurrentGeneration { get; private set; } = 0;

        public void ClearCurrentCells()
        {
            history[CurrentGeneration].Clear();
        }

        public GameOfLife(int boardWidth, int boardHeight)
        {
            history.Add(new Cells(boardWidth, boardHeight));
        }

        public void DrawTo(IRenderer renderer)
        {
            history[CurrentGeneration].Draw(renderer);
        }

        public void StepForward()
        {
            if (CurrentGeneration == Generations - 1)
            {
                history.Add(history[CurrentGeneration].Next());
            }
            CurrentGeneration++;
        }

        public void StepBackward()
        {
            if (CurrentGeneration != 0)
                CurrentGeneration--;
        }

        public void Reset()
        {
            var width = history[CurrentGeneration].Width;
            var height = history[CurrentGeneration].Height;

            history.Clear();
            history.Add(new Cells(width, height));
            CurrentGeneration = 0;
        }
    }
}
