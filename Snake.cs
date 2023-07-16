namespace ConsoleSnake;

public class Snake
{
    public Snake()
    {
        sizeY = field.SizeY;
        sizeX = field.SizeX;
        area = field.getArea();
    }

    private Area field = new Area(18, 41);
    
    private int sizeY;
    private int sizeX;

    private int curPosY;
    private int curPosX;
    
    private int cookiePosY;
    private int cookiePosX;

    private int keyPressedCount = 0;

    private char[,] area;
    
    private Queue<int> q = new();

    private int flag;
    private int snakeLength;
    private char directionFlag;
    private char previousDirectionFlag;
    private int block;

    private char endChar = 'r';

    public void Start()
    {
        while (true)
        {
            if (endChar == 'r')
            {
                GameLogic();
            }
            if (endChar == 'q') 
                break;
        }
    }

    public void GameLogic()
    {
        field.InitArea();
        q.Clear();
        curPosY = 8;
        curPosX = 16;
        flag = 1;
        snakeLength = 2;
        directionFlag = 'w';
        block = 0;

        while (true)
        {
            CookiesSpawn();
            if (curPosY < sizeY - 1 && curPosX < sizeX - 1 && curPosY > 0 && curPosX > 0)
            {
                if (!Console.KeyAvailable)
                {
                    previousDirectionFlag = directionFlag;
                    keyPressedCount = 0;
                    Move();
                    if (block == 1)
                    {
                        Console.WriteLine("YOU LOSE!\nPress 'r' to restart, 'q' to quit.");
                        endChar = Console.ReadKey(true).KeyChar;
                        break;
                    }
                }
                else
                {
                    previousDirectionFlag = directionFlag;
                    directionFlag = Console.ReadKey(true).KeyChar.ToString().ToLower()[0];
                    keyPressedCount++;
                    Move();
                    if (block == 1)
                    {
                        Console.WriteLine("YOU LOSE!\nPress 'r' to restart, 'q' to quit.");
                        endChar = Console.ReadKey(true).KeyChar;
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("YOU LOSE!\nPress 'r' to restart, 'q' to quit.");
                endChar = Console.ReadKey(true).KeyChar;
                break;
            }
        }
        
    }

    private void CookiesSpawn()
    {
        if (area[cookiePosY, cookiePosX] == '$') return;
        Random random = new Random();
        while (true)
        {
            cookiePosY = random.Next(1, sizeY - 2);
            cookiePosX = 2 * random.Next(1, (sizeX - 2) / 2);
            if (area[cookiePosY, cookiePosX] != 'o')
            {
                area[cookiePosY, cookiePosX] = '$';
                break;
            }
        }
    }
    
    private void EatCookie()
    {
        if (curPosY >= 1 && curPosY <= sizeY - 2 && curPosX <= sizeX - 3 && curPosX >= 2)
        {
            if (area[curPosY, curPosX] == '$')
                snakeLength++;
        }
    }
    
    private void AddTail()
    {
        if (area[curPosY, curPosX] == 'o')
        {
            block = 1;
            return;
        }
        area[curPosY, curPosX] = 'o';
        q.Enqueue(curPosY);
        q.Enqueue(curPosX);
        flag++;
    }
    
    private void DeleteTail()
    {
        var delY = q.Dequeue();
        var delX = q.Dequeue();
        area[delY, delX] = ' ';
        flag = snakeLength - 1;
    }
    
    
    
    private void Move()
    {
        if (directionFlag == 'w')
        {
            if (keyPressedCount > 1 && previousDirectionFlag == 'w') return;
            if (previousDirectionFlag == 's')
            {
                directionFlag = 's';
                return;
            }
            curPosY--;
        }
        else if (directionFlag == 'a')
        {
            if (keyPressedCount > 1 && previousDirectionFlag == 'a') return;
            if (previousDirectionFlag == 'd')
            {
                directionFlag = 'd';
                return;
            }
            curPosX -= 2;
        }
        else if (directionFlag == 's')
        {
            if (keyPressedCount > 1 && previousDirectionFlag == 's') return;
            if (previousDirectionFlag == 'w')
            {
                directionFlag = 'w';
                return;
            }
            curPosY++;
        }
        else if (directionFlag == 'd')
        {
            if (keyPressedCount > 1 && previousDirectionFlag == 'd') return;
            if (previousDirectionFlag == 'a')
            {
                directionFlag = 'a';
                return;
            }

            curPosX += 2;
        }
        else
        {
            directionFlag = previousDirectionFlag;
            return;
        }
        Thread.Sleep(150);
        if (flag == snakeLength)          
            DeleteTail();
        EatCookie();
        AddTail();
        if (block == 1) return;
        field.UpdateArea(snakeLength - 2);
    }
}