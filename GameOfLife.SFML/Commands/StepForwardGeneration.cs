namespace GameOfLife.SFML
{
    internal class StepForwardGeneration : ICommand
    {
        private readonly GameOfLife life;

        public StepForwardGeneration(GameOfLife life)
        {
            this.life = life;
        }

        public void Execute()
        {
            life.StepForward();
        }
    }
}
