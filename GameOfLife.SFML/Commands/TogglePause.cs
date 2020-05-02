using System;

namespace GameOfLife.SFML
{
    internal class TogglePause : ICommand
    {
        private Action togglePauseAction;

        public TogglePause(Action togglePause)
        {
            togglePauseAction = togglePause;
        }

        public void Execute()
        {
            togglePauseAction.Invoke();
        }
    }
}