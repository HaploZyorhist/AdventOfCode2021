StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();
var entries = data.Split("\r\n").ToList();

var caves = ProcessData(entries);

List<List<string>> paths = new List<List<string>>();

var start = caves.Where(x => x.Name == "start").First();

FindPaths(start);

// for part 1, remove all references to 'doubleVisit and newDoubleVisit'

Console.WriteLine($"The total number of paths is {paths.Count}");
Console.ReadKey();

void FindPaths(Cave start)
{
    foreach (var connection in start.Connections)
    {
        List<string> path = new List<string>();

        bool doubleVisit = false;

        path.Add(start.Name);

        TestPath(path, connection, doubleVisit);
    }
}

void TestPath(List<string> path, string location, bool doubleVisit)
{
    var cave = caves.Where(x => x.Name == location).First();
    List<string> PathAddition = new List<string>();
    PathAddition.AddRange(path);
    PathAddition.Add(cave.Name);

    if (PathAddition.Contains("end"))
    {
        paths.Add(PathAddition);
        return;
    }

    foreach (var connection in cave.Connections)
    {
        var newDoubleVisit = doubleVisit;

        bool canVisit = false;

        if (char.IsUpper(connection[0]))
        {
            canVisit = true;
        }
        else if (!PathAddition.Contains(connection))
        {
            canVisit = true;
        }
        else if (PathAddition.Contains(connection) && connection != "start" && !newDoubleVisit)
        {
            newDoubleVisit = true;
            canVisit = true;
        }

        if (canVisit)
        {
            TestPath(PathAddition, connection, newDoubleVisit);
        }
    }

    return;
}

List<Cave> ProcessData(List<string> entries)
{
    List<Cave> caves = new List<Cave>();

    foreach (var entry in entries)
    {
        var c = entry.Split("-");

        var firstCave = caves.Where(x => x.Name == c[0]).FirstOrDefault();
        var secondCave = caves.Where(x => x.Name == c[1]).FirstOrDefault();

        if (firstCave == null)
        {
            var cave = BuildCave(c[0], c[1]);

            caves.Add(cave);
        }
        else
        {
            if (!firstCave.Connections.Contains(c[1]))
            {
                firstCave.Connections.Add(c[1]);
            }
        }

        if (secondCave == null)
        {
            var cave = BuildCave(c[1], c[0]);

            caves.Add(cave);
        }
        else
        {
            if (!secondCave.Connections.Contains(c[0]))
            {
                secondCave.Connections.Add(c[0]);
            }
        }
    }

    return caves;
}

Cave BuildCave(string caveName, string caveConnection)
{
    Cave cave = new Cave
    {
        Connections = new List<string>()
    };
    cave.Name = caveName;
    cave.Connections.Add(caveConnection);

    if (char.IsUpper(caveName[0]))
    {
        cave.Large = true;
    }
    else
    {
        cave.Large = false;
    }

    return cave;
}

public class Cave
{
    public string Name { get; set; }
    public List<string> Connections { get; set; }
    public bool Large { get; set; }
}