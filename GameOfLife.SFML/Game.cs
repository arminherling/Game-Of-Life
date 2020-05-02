﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace GameOfLife.SFML
{
    public class Game
    {
        const int rows = 124;
        const int columns = 86;
        Color clearColor = new Color(116, 195, 101);
        RenderWindow window;
        Queue<ICommand> commands = new Queue<ICommand>();
        GameOfLife life;
        Clock clock = new Clock();
        GraphicsRenderer renderer;

        public Game(uint width, uint height, string title)
        {
            window = new RenderWindow(new VideoMode(width, height), title);
            window.Closed += (_, __) => commands.Enqueue(new CloseWindow(window));
            window.KeyPressed += HandleKeyPressed;
            life = new GameOfLife(rows, columns);
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
