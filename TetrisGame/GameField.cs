using System;

class GameField
{
    private int[,] FieldMatrix;
    private int Rows;
    private int Cols;

    public GameField(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        FieldMatrix = new int[rows, cols];
    }

    public bool CanMoveShape(FallingShape shape, int deltaX, int deltaY)
    {
        for (int i = 0; i < shape.ShapeMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < shape.ShapeMatrix.GetLength(1); j++)
            {
                if (shape.ShapeMatrix[i, j] == 1)
                {
                    int newX = shape.X + j + deltaX;
                    int newY = shape.Y + i + deltaY;

                    if (newX < 0 || newX >= Cols || newY >= Rows || (newY >= 0 && FieldMatrix[newY, newX] == 1))
                        return false;
                }
            }
        }
        return true;
    }

    public bool CanRotateShape(FallingShape shape)
    {
        int[,] rotatedShape = shape.GetRotatedShape();

        for (int i = 0; i < rotatedShape.GetLength(0); i++)
        {
            for (int j = 0; j < rotatedShape.GetLength(1); j++)
            {
                if (rotatedShape[i, j] == 1)
                {
                    int newX = shape.X + j;
                    int newY = shape.Y + i;

                    if (newX < 0 || newX >= Cols || newY >= Rows || (newY >= 0 && FieldMatrix[newY, newX] == 1))
                        return false;
                }
            }
        }
        return true;
    }

    public void PlaceShape(FallingShape shape)
    {
        for (int i = 0; i < shape.ShapeMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < shape.ShapeMatrix.GetLength(1); j++)
            {
                if (shape.ShapeMatrix[i, j] == 1)
                {
                    FieldMatrix[shape.Y + i, shape.X + j] = 1;
                }
            }
        }
    }

    public bool IsGameOver()
    {
        for (int j = 0; j < Cols; j++)
        {
            if (FieldMatrix[0, j] == 1)
                return true;
        }
        return false;
    }

    public void DrawField()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                Console.SetCursorPosition(j, i);
                Console.Write(FieldMatrix[i, j] == 1 ? "#" : ".");
            }
        }
    }
}
