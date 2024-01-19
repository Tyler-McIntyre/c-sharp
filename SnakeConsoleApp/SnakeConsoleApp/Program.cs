using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        SnakeGame game = new SnakeGame();
        game.Init();

        while (!game.GameOver)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                game.ChangeDirection(key.Key);
            }
            game.Update();
            Thread.Sleep(100); // Adjust the game speed
        }

        Console.WriteLine("Game Over!");
    }
}
