﻿namespace GameOfLife.SFML
{
    internal class ResetGame : ICommand
    {
        private GameOfLife life;

        public ResetGame(GameOfLife life)
        {
            this.life = life;
        }

        public void Execute()
        {
            life.Reset();
        }
    }
}