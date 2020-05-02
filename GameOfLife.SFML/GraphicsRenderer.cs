using SFML.Graphics;
using SFML.System;

namespace GameOfLife.SFML
{
    public class GraphicsRenderer : IRenderer, Drawable
    {
        private readonly int gridSize = 8;
        private readonly int cellSize = 7;
        private readonly int verticesPerCell = 6;
        private readonly Vector2i origin;
        private readonly int rows;
        private readonly int columns;
        Vertex[] vertices;

        public GraphicsRenderer(Vector2i origin, int rows, int columns)
        {
            this.origin = origin;
            this.rows = rows;
            this.columns = columns;
            vertices = new Vertex[rows * columns * verticesPerCell];

            for (int y = 0; y < columns; y++)
            {
                for (int x = 0; x < rows; x++)
                {
                    var x1 = x * gridSize + origin.X;
                    var x2 = x1 + cellSize;
                    var y1 = y * gridSize + origin.Y;
                    var y2 = y1 + cellSize;

                    int cell = verticesPerCell * (x + rows * y);

                    vertices[cell].Position = new Vector2f(x1, y1);
                    vertices[cell].Color = Color.White;
                    vertices[cell + 1].Position = new Vector2f(x2, y1);
                    vertices[cell + 1].Color = Color.White;
                    vertices[cell + 2].Position = new Vector2f(x1, y2);
                    vertices[cell + 2].Color = Color.White;
                    vertices[cell + 3].Position = new Vector2f(x2, y1);
                    vertices[cell + 3].Color = Color.White;
                    vertices[cell + 4].Position = new Vector2f(x1, y2);
                    vertices[cell + 4].Color = Color.White;
                    vertices[cell + 5].Position = new Vector2f(x2, y2);
                    vertices[cell + 5].Color = Color.White;
                }
            }
        }

        public Vector2i? PositionToCell(Vector2i position)
        {
            var originPosition = position - origin;

            var cellIndexX = originPosition.X / gridSize;
            var cellIndexY = originPosition.Y / gridSize;

            var onGridX = cellIndexX >= 0 && cellIndexX < rows;
            var onGridY = cellIndexY >= 0 && cellIndexY < columns;

            if (onGridX && onGridY)
                return new Vector2i(cellIndexX, cellIndexY);

            return null;
        }

        public void Draw(in CellState[,] cellState, int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int cell = verticesPerCell * (x + width * y);

                    var color = cellState[x, y] == CellState.Alive
                        ? Color.Black
                        : Color.White;

                    vertices[cell].Color = color;
                    vertices[cell+1].Color = color;
                    vertices[cell+2].Color = color;
                    vertices[cell+3].Color = color;
                    vertices[cell+4].Color = color;
                    vertices[cell+5].Color = color;
                }
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(vertices, PrimitiveType.Triangles);
        }
    }
}
