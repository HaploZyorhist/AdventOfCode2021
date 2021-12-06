StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();

var scans = data.Split("\r\n").ToList();

var processedData = ProcessData(scans);

Dictionary<(int, int), int> map = new Dictionary<(int, int), int>();

int highCount = 0;

foreach (var c in processedData)
{
    int xStep = 0;
    int yStep = 0;

    // part 1
    //if (c.X1 == c.X2 ||
    //    c.Y1 == c.Y2)
    //{
    if (c.X1 > c.X2)
    {
        xStep = -1;
    }
    else if (c.X1 < c.X2)
    {
        xStep = 1;
    }

    if (c.Y1 > c.Y2)
    {
        yStep = -1;
    }
    else if (c.Y1 < c.Y2)
    {
        yStep = 1;
    }

    int x = c.X1;
    int y = c.Y1;

    while (true)
    {
        if (!map.ContainsKey((x, y)))
        {
            map.Add((x, y), 0);
        }
        else
        {
            map[(x, y)]++;
        }

        if ((x, y) == (c.X2, c.Y2))
        {
            break;
        }

        x += xStep;
        y += yStep;
    }
    //}
}

highCount = map.Count(x => x.Value > 0);

Console.WriteLine($"The number of high fields are {highCount}");
Console.ReadKey();

List<Line> ProcessData(List<string> scans)
{
    List<Line> result = new List<Line>();

    foreach (string s in scans)
    {
        var coord = s.Split(" -> ");

        var startCoord = coord[0].Split(",");
        var endCoord = coord[1].Split(",");

        Line line = new Line
        {
            X1 = int.Parse(startCoord[0]),
            Y1 = int.Parse(startCoord[1]),
            X2 = int.Parse(endCoord[0]),
            Y2 = int.Parse(endCoord[1])
        };

        result.Add(line);
    }

    return result;
}
public class Line
{
    public int X1 { get; set; }
    public int X2 { get; set; }
    public int Y1 { get; set; }
    public int Y2 { get; set; }
}