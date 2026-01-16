int windowWidth = 60;
int windowHeight = 20;
Console.BufferWidth = Console.WindowWidth = windowWidth;
Console.BufferHeight = Console.WindowHeight = windowHeight;
Console.CursorVisible = false;

int dwarfX = windowWidth / 2;
int dwarfY = windowHeight - 1;
string dwarf = "(0)";
Random randomGenerator = new Random();

List<int> rocksX = new List<int>();
List<int> rocksY = new List<int>();
List<char> rocksChar = new List<char>();
List<ConsoleColor> rocksColor = new List<ConsoleColor>();

char[] availableRocks = { '^', '@', '*', '&', '+', '%', '$', '#', '!', '.', ';' };
ConsoleColor[] availableColors = { ConsoleColor.Cyan, ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.Red };

while (true)
{
    int spawnChance = randomGenerator.Next(0, 100);
    if (spawnChance < 30)
    {
        rocksX.Add(randomGenerator.Next(0, windowWidth));
        rocksY.Add(0);
        rocksChar.Add(availableRocks[randomGenerator.Next(0, availableRocks.Length)]);
        rocksColor.Add(availableColors[randomGenerator.Next(0, availableColors.Length)]);
    }

    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo pressedKey = Console.ReadKey(true);
        if (pressedKey.Key == ConsoleKey.LeftArrow)
            if (dwarfX > 0) dwarfX--;
        if (pressedKey.Key == ConsoleKey.RightArrow)
            if (dwarfX < windowWidth - 3) dwarfX++;
    }

    List<int> newListX = new List<int>();
    List<int> newListY = new List<int>();
    List<char> newListChar = new List<char>();
    List<ConsoleColor> newListColor = new List<ConsoleColor>();

    for (int i = 0; i < rocksY.Count; i++)
    {
        int currentX = rocksX[i];
        int currentY = rocksY[i];
        char currentChar = rocksChar[i];
        ConsoleColor currentColor = rocksColor[i];

        if (currentY == dwarfY && (currentX >= dwarfX && currentX <= dwarfX + 2))
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("GAME OVER!");
            return;
        }

        if (currentY < windowHeight - 1)
        {
            newListX.Add(currentX);
            newListY.Add(currentY + 1);
            newListChar.Add(currentChar);
            newListColor.Add(currentColor);
        }
    }

    rocksX = newListX;
    rocksY = newListY;
    rocksChar = newListChar;
    rocksColor = newListColor;

    Console.Clear();

    Console.SetCursorPosition(dwarfX, dwarfY);
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write(dwarf);

    for (int i = 0; i < rocksX.Count; i++)
    {
        Console.SetCursorPosition(rocksX[i], rocksY[i]);
        Console.ForegroundColor = rocksColor[i];
        Console.Write(rocksChar[i]);
    }

    System.Threading.Thread.Sleep(150);
}
