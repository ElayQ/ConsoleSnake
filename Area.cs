namespace ConsoleSnake;

public class Area
{
    private int sizeY;
    private int sizeX;
    
    private char[,] area;
    
    public Area(int sizeY, int sizeX)
    {
        this.sizeY = sizeY;
        this.sizeX = sizeX;
        area = new char[sizeY, sizeX];
    }

    public int SizeY => sizeY;

    public int SizeX => sizeX;

    public char[,] getArea() => area;

    private void SetBorders()
    {
        for (int i = 0; i < sizeX; i += 2)
        {
            area[0, i] = '*';
        }

        for (int i = 0; i < sizeY; i++)
        {
            area[i, 0] = '*';
        }

        for (int i = 0; i < sizeX; i += 2)
        {
            area[sizeY - 1, i] = '*';
        }

        for (int i = 0; i < sizeY; i++)
        {
            area[i, sizeX - 1] = '*';
        }
    }
    
    public void InitArea()
    {
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                area[i, j] = ' ';
            }
        }
        SetBorders();

        /*area[curPosY, curPosX] = 'o';
        q.Enqueue(curPosY);
        q.Enqueue(curPosX);*/

        UpdateArea(0);
    }
    
    public void UpdateArea(int score)
    {
        Console.Clear();
        using var output = new StreamWriter(Console.OpenStandardOutput());
        
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                output.Write(area[i, j]);
            }
            output.WriteLine();
        }
        output.WriteLine($"Current score: {score}");
    }
}