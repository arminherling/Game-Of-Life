using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace GameOfLife.SFML
{
    public class Game
    {
        private const int rows = 124;
        private const int columns = 86;
        private Clock clock = new Clock();
        private Color clearColor = new Color(116, 195, 101);
        private RenderWindow window;
        private GameOfLife life;
        private GraphicsRenderer renderer;
        private GenerationTrackBar generationDisplay;
        private Queue<ICommand> commands = new Queue<ICommand>();
        private bool isPlaying = false;
        private Text leftInstructions;
        private Text rightInstructions;

        public Game(uint width, uint height, string title)
        {
            window = new RenderWindow(new VideoMode(width, height), title);
            window.Closed += (_, __) => commands.Enqueue(new CloseWindow(window));
            window.KeyPressed += HandleKeyPressed;
            life = new GameOfLife(rows, columns);

            var font = new Font("Font/OpenSans-Regular.ttf");
            var fontSize = 24u;
            generationDisplay = new GenerationTrackBar(new Vector2f(width / 2, height - 20), new Vector2f(512, 8), font, fontSize, life);
            renderer = new GraphicsRenderer(new Vector2i(16, 16), rows, columns);

            leftInstructions = new Text("Reset (R)   Clear (C)\nContinue (Space)", font, fontSize)
            {
                Position = new Vector2f(16, height - 65),
                OutlineColor = Color.Black,
                OutlineThickness = 2
            };

            rightInstructions = new Text("Forward (Right)\nBackward (Left)", font, fontSize)
            {
                OutlineColor = Color.Black,
                OutlineThickness = 2
            };
            var rightInstructionWidth = rightInstructions.GetLocalBounds().Width;
            rightInstructions.Position = new Vector2f(width - rightInstructionWidth - 16, height - 65);
        }

        private void HandleKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
                commands.Enqueue(new CloseWindow(window));
            else if (e.Code == Keyboard.Key.Right)
                commands.Enqueue(new StepForwardGeneration(life));
            else if (e.Code == Keyboard.Key.Left)
                commands.Enqueue(new StepBackwardGeneration(life));
            else if (e.Code == Keyboard.Key.Space)
                commands.Enqueue(new TogglePause(TogglePause));
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
            if (IsPlaying() && clock.ElapsedTime.AsSeconds() > 0.15f)
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
            window.Draw(leftInstructions);
            window.Draw(rightInstructions);
            window.Display();
        }

        private void ExecuteCommands()
        {
            while (commands.TryDequeue(out ICommand command))
            {
                command.Execute();
            }
        }

        public bool IsPlaying() => isPlaying;

        public void TogglePause()
        {
            isPlaying = !isPlaying;

            if (isPlaying)
                leftInstructions.DisplayedString = "Reset (R)   Clear (C)\nPause (Space)";
            else
                leftInstructions.DisplayedString = "Reset (R)   Clear (C)\nContinue (Space)";
        }
    }
}
