namespace Tanks
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game()) 
            {
                game.Run();
            }
        }
    }
}
