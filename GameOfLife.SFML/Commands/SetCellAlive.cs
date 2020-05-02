using SFML.System;
using System.Drawing;

namespace GameOfLife.SFML
{
    internal class SetCellAlive : ICommand
    {
        private readonly GameOfLife life;
        private readonly GraphicsRenderer renderer;
        private Vector2i position;

        public SetCellAlive(GameOfLife life, GraphicsRenderer renderer, Vector2i position)
        {
            this.life = life;
            this.renderer = renderer;
            this.position = position;
        }

        public void Execute()
        {
            var optionalCellIndex = renderer.PositionToCell(position);
            if (optionalCellIndex == null)
                return;

            var cellIndex = optionalCellIndex.Value;
            life.SetCellAlive(new Point(cellIndex.X, cellIndex.Y));
        }
    }
}