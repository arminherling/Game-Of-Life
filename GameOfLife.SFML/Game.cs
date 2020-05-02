using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace GameOfLife.SFML
{
    public class Game
    {
        const int rows = 124;
        const int columns = 86;
        Clock clock = new Clock();
        Color clearColor = new Color(116, 195, 101);
        RenderWindow window;
        GameOfLife life;
        GraphicsRenderer renderer;
        GenerationTrackBar generationDisplay;
        Queue<ICommand> commands = new Queue<ICommand>();

        public Game(uint width, uint height, string title)
        {
            window = new RenderWindow(new VideoMode(width, height), title);
            window.Closed += (_, __) => commands.Enqueue(new CloseWindow(window));
            window.KeyPressed += HandleKeyPressed;
            life = new GameOfLife(rows, columns);
            generationDisplay = new GenerationTrackBar(new Vector2f(width/2, height - 20), new Vector2f(512, 8), life);
            renderer = new GraphicsRenderer(new Vector2i(16,16), rows, columns);
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
            clock.Restart(); 
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
            life.DrawTo(renderer);
            window.Draw(renderer);
            window.Draw(generationDisplay);
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
