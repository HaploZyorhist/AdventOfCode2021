using System.Text;

StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();
var entries = data.Split("\r\n\r\n").ToList();

var map = ProcessMapData(entries[0]);
var instructions = ProcessInstructionData(entries[1]);

int instructionsToPerform = 1; // 1 for part 1

// part 2
instructionsToPerform = instructions.Count;

for (int i = 0; i < instructionsToPerform; i++)
{
    map = PerformInstructions(map, instructions[i]);
}

var totalDots = map.Where(x => x.Value > 0).ToList().Count();

var display = GetPrintout(map);

Console.WriteLine($"The total number of empty spaces after {instructionsToPerform} folds, is {totalDots}");
Console.Write(display);
Console.ReadKey();

string GetPrintout(Dictionary<(int, int), int> map)
{
    StringBuilder sb = new StringBuilder();

    int mapHeight = map.Where(x => x.Key.Item1 == 0).ToList().Count;
    int mapWidth = map.Where(x => x.Key.Item2 == 0).ToList().Count;

    for (int y = 0; y < mapHeight; y++)
    {
        for (int x = 0; x < mapWidth; x++)
        {
            if (map[(x, y)] > 0)
            {
                sb.Append("#");
            }
            else
            {
                sb.Append(" ");
            }
        }
        sb.AppendLine();
    }

    return sb.ToString();
}

Dictionary<(int, int), int> PerformInstructions(Dictionary<(int, int), int> map, (string, int) instructions)
{
    Dictionary<(int, int), int> UpdatedMap = new Dictionary<(int, int), int>();

    string axis = instructions.Item1;
    int line = instructions.Item2;

    int mapHeight = map.Where(x => x.Key.Item1 == 0).ToList().Count;
    int mapWidth = map.Where(x => x.Key.Item2 == 0).ToList().Count;

    if (axis == "y")
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (y == line)
                {
                    continue;
                }
                if (y < line)
                {
                    UpdatedMap.Add((x, y), map[(x, y)]);
                }
                else if (y > line)
                {
                    var offset = (y - line) * 2;
                    UpdatedMap[(x, (y - offset))] += map[(x, y)];
                }
            }
        }
    }
    else if (axis == "x")
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (x == line)
                {
                    continue;
                }
                if (x < line)
                {
                    UpdatedMap.Add((x, y), map[(x, y)]);
                }
                else if (x > line)
                {
                    var offset = (x - line) * 2;
                    UpdatedMap[((x - offset), y)] += map[(x, y)];
                }
            }
        }
    }

    return UpdatedMap;
}

Dictionary<(int, int), int> ProcessMapData(string data)
{
    Dictionary<(int, int), int> map = new Dictionary<(int, int), int>();
    List<(int, int)> list = new List<(int, int)>();

    int largestX = 0;
    int largestY = 0;

    var gridlines = data.Split("\r\n");

    foreach (var line in gridlines)
    {
        var l = line.Split(",");

        var x = int.Parse(l[0]);
        var y = int.Parse(l[1]);

        if (x > largestX)
        {
            largestX = x;
        }
        if (y > largestY)
        {
            largestY = y;
        }

        list.Add((x, y));
    }

    for (int x = 0; x <= largestX; x++)
    {
        for (int y = 0; y <= largestY; y++)
        {
            var d = 0;

            if (list.Contains((x, y)))
            {
                d = 1;
            }

            map.Add((x, y), d);
        }
    }

    return map;
}

List<(string, int)> ProcessInstructionData(string data)
{
    List<(string, int)> list = new List<(string, int)>();

    foreach (var line in data.Split("\r\n"))
    {
        var l = line.Split(" ");

        var g = l[2].Split("=");

        var str = g[0];
        var length = int.Parse((g[1]));

        list.Add((str, length));
    }

    return list;
}