namespace GameOfLife.SFML
{
    internal class ClearCells : ICommand
    {
        private readonly GameOfLife life;

        public ClearCells(GameOfLife life)
        {
            this.life = life;
        }

        public void Execute()
        {
            life.ClearCurrentCells();
        }
    }
}