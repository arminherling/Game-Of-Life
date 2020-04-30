using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace GameOfLife.SFML
{
    public class Game
    {
        Color clearColor;
        RenderWindow window;
        Queue<ICommand> commands = new Queue<ICommand>();
        GameOfLife life;
        Clock clock;

        public Game(uint width, uint height, string title)
        {
            clearColor = new Color(116, 195, 101);
            window = new RenderWindow(new VideoMode(width, height), title);
            window.Closed += (_, __) => commands.Enqueue(new CloseWindow(window));
            window.KeyPressed += HandleKeyPressed;
            life = new GameOfLife(100, 100);
        }

        private void HandleKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
                commands.Enqueue(new CloseWindow(window));
            else if (e.Code == Keyboard.Key.Right)
                commands.Enqueue(new StepForwardGeneration(life));
            else if (e.Code == Keyboard.Key.Left)
                commands.Enqueue(new StepBackwardGeneration(life));
        }

        public void Run()
        {
            clock = new Clock();
            while (window.IsOpen)
            {
                window.DispatchEvents();
                Update();
                ExecuteCommands();
                Draw();
            }
        }

        private void Update()
        {
            if(clock.ElapsedTime.AsSeconds() > 0.15f)
            {
                clock.Restart();
                commands.Enqueue(new StepForwardGeneration(life));
            }
        }

        private void Draw()
        {
            window.Clear(clearColor);
            // TODO draw the cells
            window.Display();
        }

        private void ExecuteCommands()
        {
            while(commands.TryDequeue(out ICommand command))
            {
                command.Execute();
            }
        }
    }
}
