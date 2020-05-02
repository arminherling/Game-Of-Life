using SFML.Graphics;
using SFML.System;
using System;

namespace GameOfLife.SFML
{
    class GenerationTrackBar : Drawable
    {
        private GameOfLife life;
        private RectangleShape bar;
        private FloatRect barBounds;
        private RectangleShape slider;
        private Text generationText;

        public GenerationTrackBar(Vector2f barPosition, Vector2f barSize, Font font, uint fontSize, GameOfLife life)
        {
            this.life = life;
            bar = new RectangleShape(barSize)
            {
                Origin = barSize / 2,
                Position = barPosition
            };
            barBounds = bar.GetGlobalBounds();

            var sliderSize = new Vector2f(barSize.Y, barSize.Y * 3);
            slider = new RectangleShape(sliderSize)
            {
                Origin = sliderSize / 2,
                Position = new Vector2f(barBounds.Left, bar.Position.Y)
            };

            generationText = new Text("", font, fontSize)
            {
                Position = new Vector2f(400, bar.Position.Y-45),
                OutlineColor = Color.Black,
                OutlineThickness = 2f
            };
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            var percentage = (float)(life.CurrentGeneration) / (life.Generations - 1);
            if (float.IsNaN(percentage))
                percentage = 0;

            generationText.DisplayedString = $"Generation {life.CurrentGeneration} / {life.Generations - 1}";
            target.Draw(generationText, states);
            target.Draw(bar, states);
            
            var sliderNewX = barBounds.Left + (barBounds.Left + barBounds.Width - barBounds.Left) * percentage;
            slider.Position = new Vector2f(sliderNewX, slider.Position.Y);
            target.Draw(slider, states);
        }

        internal void Update()
        {
            throw new NotImplementedException();
        }
    }
}
