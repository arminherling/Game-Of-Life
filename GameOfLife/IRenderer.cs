namespace GameOfLife
{
    public interface IRenderer
    {
        void Draw(in CellState[,] cellState, int width, int height);
    }
}
