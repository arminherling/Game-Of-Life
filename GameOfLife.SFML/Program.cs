namespace GameOfLife.SFML
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(1024, 768, "Game Of Life");
            game.Run();
        }
    }
}
