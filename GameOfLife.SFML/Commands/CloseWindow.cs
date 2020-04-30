using SFML.Graphics;

namespace GameOfLife.SFML
{
    internal class CloseWindow : ICommand
    {
        private readonly RenderWindow window;

        public CloseWindow(RenderWindow window)
        {
            this.window = window;
        }

        public void Execute()
        {
            window.Close();
        }
    }
}
