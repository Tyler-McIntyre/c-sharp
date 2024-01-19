using System;
using System.Collections.Generic;

public class SnakeGame
{
    private List<Point> snake;
    private Point food;
    private int boardWidth = 30;
    private int boardHeight = 30;
    private bool gameOver;
    private Direction currentDirection;

    private enum Direction { Up, Down, Left, Right }

    public bool GameOver { get { return gameOver; } }

    public void Init()
    {
        gameOver = false;
        currentDirection = Direction.Right;
        snake = new List<Point> { new Point(5, 5) };
        GenerateFood();
    }

    public void Update()
    {
        MoveSnake();

        // food collision detection
        if (snake[0].X == food.X && snake[0].Y == food.Y)
        {
            GenerateFood();
        }

        if (snake[0].X < 0 || snake[0].X >= boardWidth || snake[0].Y < 0 || snake[0].Y >= boardHeight)
        {
            gameOver = true;
        }

        for (int i = 1; i < snake.Count; i++)
        {
            // checks if any point of the body is touching
            if (snake[i].X == snake[0].X && snake[i].Y == snake[0].Y)
            {
                gameOver = true;
            }
        }

        DrawBoard();
    }

    public void ChangeDirection(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.W:
                if (currentDirection != Direction.Down) currentDirection = Direction.Up;
                break;
            case ConsoleKey.S:
                if (currentDirection != Direction.Up) currentDirection = Direction.Down;
                break;
            case ConsoleKey.A:
                if (currentDirection != Direction.Right) currentDirection = Direction.Left;
                break;
            case ConsoleKey.D:
                if (currentDirection != Direction.Left) currentDirection = Direction.Right;
                break;
        }
    }

    private void MoveSnake()
    {
        Point head = new Point(snake[0].X, snake[0].Y);

        switch (currentDirection)
        {
            case Direction.Up:
                head.Y--;
                break;
            case Direction.Down:
                head.Y++;
                break;
            case Direction.Left:
                head.X--;
                break;
            case Direction.Right:
                head.X++;
                break;
        }

        snake.Insert(0, head);
        if (snake[0].X != food.X || snake[0].Y != food.Y) // Only remove tail if not eaten food
        {
            snake.RemoveAt(snake.Count - 1);
        }
    }

    private void GenerateFood()
    {
        Random rnd = new Random();
        food = new Point(rnd.Next(boardWidth), rnd.Next(boardHeight));
    }

    private void DrawBoard()
    {
        Console.Clear();

        for (int y = 0; y < boardHeight; y++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                if (x == food.X && y == food.Y)
                {
                    Console.Write("F");
                }
                else if (snake.Exists(p => p.X == x && p.Y == y))
                {
                    Console.Write("S");
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }
    }
}
