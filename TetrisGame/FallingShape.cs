using System;

class FallingShape
{
    public int[,] ShapeMatrix { get; private set; }
    public int X { get; set; }
    public int Y { get; set; }

    private static readonly int[][,] Shapes = new int[][,]
    {
        new int[,] { { 1, 1, 1 }, { 0, 1, 0 } }, // T-образная фигура
        new int[,] { { 1, 1, 1, 1 } },           // Прямая линия
        new int[,] { { 1, 1 }, { 1, 1 } },       // Квадрат
        new int[,] { { 0, 1, 1 }, { 1, 1, 0 } }, // Z-образная фигура
        new int[,] { { 1, 1, 0 }, { 0, 1, 1 } }, // Обратная Z-образная фигура
        new int[,] { { 1, 1, 1 }, { 1, 0, 0 } }, // L-образная фигура
        new int[,] { { 1, 1, 1 }, { 0, 0, 1 } }  // Обратная L-образная фигура
    };

    public FallingShape()
    {
        ShapeMatrix = Shapes[new Random().Next(Shapes.Length)];
        X = 4; // Начальная позиция по горизонтали
        Y = 0; // Начальная позиция по вертикали
    }

    public int[,] GetRotatedShape()
    {
        int rows = ShapeMatrix.GetLength(0);
        int cols = ShapeMatrix.GetLength(1);
        int[,] rotated = new int[cols, rows];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                rotated[j, rows - 1 - i] = ShapeMatrix[i, j];

        return rotated;
    }

    public void ApplyRotation(int[,] rotatedShape)
    {
        ShapeMatrix = rotatedShape;
    }

    public void MoveDown() => Y++;
    public void MoveLeft() => X--;
    public void MoveRight() => X++;

    public void DrawShape()
    {
        for (int i = 0; i < ShapeMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < ShapeMatrix.GetLength(1); j++)
            {
                if (ShapeMatrix[i, j] == 1)
                {
                    Console.SetCursorPosition(X + j, Y + i);
                    Console.Write("#");
                }
            }
        }
    }
}
