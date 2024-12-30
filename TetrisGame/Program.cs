using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.CursorVisible = false;
        GameField gameField = new GameField(20, 10);
        FallingShape currentShape = new FallingShape();
        bool isGameOver = false;

        Thread inputThread = new Thread(() =>
        {
            while (!isGameOver)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            if (gameField.CanMoveShape(currentShape, -1, 0))
                                currentShape.MoveLeft();
                            break;
                        case ConsoleKey.RightArrow:
                            if (gameField.CanMoveShape(currentShape, 1, 0))
                                currentShape.MoveRight();
                            break;
                        case ConsoleKey.DownArrow:
                            if (gameField.CanMoveShape(currentShape, 0, 1))
                                currentShape.MoveDown();
                            break;
                        case ConsoleKey.Spacebar:
                            if (gameField.CanRotateShape(currentShape))
                                currentShape.ApplyRotation(currentShape.GetRotatedShape());
                            break;
                    }
                }
            }
        });
        inputThread.Start();

        while (!isGameOver)
        {
            Console.Clear();
            gameField.DrawField();
            currentShape.DrawShape();

            if (!gameField.CanMoveShape(currentShape, 0, 1))
            {
                gameField.PlaceShape(currentShape);
                if (gameField.IsGameOver())
                {
                    isGameOver = true;
                }
                else
                {
                    currentShape = new FallingShape();
                }
            }
            else
            {
                currentShape.MoveDown();
            }

            Thread.Sleep(500);
        }

        Console.Clear();
        Console.WriteLine("Game Over! Press R to restart.");
        if (Console.ReadKey(true).Key == ConsoleKey.R)
        {
            Main();
        }
    }
}
