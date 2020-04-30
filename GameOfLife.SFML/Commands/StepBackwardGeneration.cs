namespace GameOfLife.SFML
{
    internal class StepBackwardGeneration : ICommand
    {
        private readonly GameOfLife life;

        public StepBackwardGeneration(GameOfLife life)
        {
            this.life = life;
        }

        public void Execute()
        {
            life.StepBackward();
        }
    }
}
