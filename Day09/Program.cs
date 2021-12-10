StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();
var entries = data.Split("\r\n").ToList();

const int fieldLength = 100; // 5 for test, 100 for actual
const int fieldWidth = 100; // 10 for test, 100 for actual

var d = entries[0].Length;
var field = new int[fieldWidth, fieldLength];
int riskAssessment = 0;

Dictionary<(int, int), int> lowSpots = new Dictionary<(int, int), int>();

for (int x = 0; x < fieldWidth; x++)
{
    for (int y = 0; y < fieldLength; y++)
    {
        field[x, y] = int.Parse(entries[y][x].ToString());
    }
}

for (int x = 0; x < fieldWidth; x++)
{
    for (int y = 0; y < fieldLength; y++)
    {
        int northTile = -1;
        int southTile = -1;
        int westTile = -1;
        int eastTile = -1;

        if (y == 0)
        {
            northTile = 9;
        }
        else if (y == fieldLength - 1)
        {
            southTile = 9;
        }
        if (x == 0)
        {
            westTile = 9;
        }
        else if (x == fieldWidth - 1)
        {
            eastTile = 9;
        }
        if (northTile == -1)
        {
            northTile = field[x, y - 1];
        }
        if (southTile == -1)
        {
            southTile = field[x, y + 1];
        }
        if (westTile == -1)
        {
            westTile = field[x - 1, y];
        }
        if (eastTile == -1)
        {
            eastTile = field[x + 1, y];
        }

        if (field[x, y] < northTile &&
            field[x, y] < southTile &&
            field[x, y] < westTile &&
            field[x, y] < eastTile)
        {
            lowSpots.Add((x, y), field[x, y]);
        }
    }
}

foreach (var spot in lowSpots)
{
    riskAssessment += spot.Value + 1;
}

Console.WriteLine($"The total risk in the field is {riskAssessment}");

int biggestRisk = 0;

List<Dictionary<(int, int), int>> basins = new List<Dictionary<(int, int), int>>();

foreach (var spot in lowSpots)
{
    int x;
    int y;
    (x, y) = spot.Key;

    Dictionary<(int, int), int> basin = new Dictionary<(int, int), int>();
    basin = BasinBuilder(basin, x, y);

    if (!basins.Contains(basin))
    {
        basins.Add(basin);
    }
}

List<int> risks = new List<int>();

foreach(var basin in basins)
{
    int risk = basin.Count;
    risks.Add(risk);
}

risks.Sort();
risks.Reverse();

biggestRisk = risks[0] * risks[1] * risks[2];

Console.WriteLine($"The total risk value is {biggestRisk}");
Console.ReadKey();

Dictionary<(int, int), int> BasinBuilder(Dictionary<(int, int), int> basin, int Spotx, int Spoty)
{
    var spots = GetNeighbors(Spotx, Spoty);

    foreach (var spot in spots)
    {
        if (field[spot.x, spot.y] != 9 && !basin.ContainsKey((spot.x, spot.y)))
        {
            basin.TryAdd((spot.x, spot.y), field[spot.x, spot.y]);
            var moarSpots = BasinBuilder(basin, spot.x, spot.y);

            foreach (var s in moarSpots)
            {
                basin.TryAdd(s.Key, s.Value);
            }
        }
    }

    return basin;
}

List<(int x, int y)> GetNeighbors(int x, int y)
{
    List<(int, int)> neighbors = new List<(int, int)>();
    if (x != 0)
    {
        neighbors.Add((x - 1, y));
    }
    if (y != 0)
    {
        neighbors.Add((x, y - 1));
    }
    if (x != fieldWidth - 1)
    {
        neighbors.Add((x + 1, y));
    }
    if (y != fieldLength - 1)
    {
        neighbors.Add((x, y + 1));
    }

    return neighbors;
}