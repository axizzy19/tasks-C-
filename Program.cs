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
            ConsoleKeyInfo key = Console.ReadKey(true);
            game.HandleInput(key);
        }
    }
}