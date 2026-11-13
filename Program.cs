using ConsoleApp1;

class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;
        var game = new Game();
        game.Start();

        while (true)
        {
            if (game.isGameOver)
            {
                break;
            }

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                game.HandleInput(key);
            }
        }

        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("GAME OVER");
        Console.ReadLine();
    }
}