StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();
var entries = data.Split("\r\n").ToList();

var octo = GenerateOctopi(entries);
int flashes = 0;
int stepFlashes = 0;
int step = 0;
int unisonStep = 0;

// part 1
//for (int i = 0; i < 100; i++)
//{
//    PerformStep();
//}

while (unisonStep == 0)
{
    PerformStep();
}

Console.WriteLine($"The total number of flashes is {flashes}");
Console.WriteLine($"The step where all octopus flashed is {unisonStep}");
Console.ReadKey();

void PerformStep()
{
    step++;
    stepFlashes = 0;
    foreach (var oct in octo)
    {
        oct.Energy += 1;
        oct.Flashed = false;
    }

    foreach (var oct in octo)
    {
        if (oct.Energy > 9 && !oct.Flashed)
        {
            Flash(oct);
        }
    }

    foreach(var oct in octo)
    {
        if (oct.Energy > 9)
        {
            oct.Energy = 0;
        }
    }

    if (stepFlashes == 100)
    {
        unisonStep = step;
    }

    flashes += stepFlashes;
}

void Flash(Octopus oct)
{
    int x = oct.X;
    int y = oct.Y;
    int leftX = -1;
    int topY = -1;
    int rightX = -1;
    int bottomY = -1;
    oct.Flashed = true;
    stepFlashes++;

    if (x != 0)
    {
        leftX = x - 1;
    }
    if (y != 0)
    {
        topY = y - 1;
    }
    if (x != 9)
    {
        rightX = x + 1;
    }
    if (y != 9)
    {
        bottomY = y + 1;
    }

    // up and left octo
    if (leftX != -1 && topY != -1)
    {
        var o = octo.Where(x => x.X == leftX && x.Y == topY).First();
        o.Energy++;
        if (!o.Flashed &&
            o.Energy > 9)
        {
            Flash(o);
        }
    }

    // left octo
    if (leftX != -1)
    {
        var o = octo.Where(o => o.X == leftX && o.Y == y).First();
        o.Energy++;
        if (!o.Flashed &&
            o.Energy > 9)
        {
            Flash(o);
        }
    }

    // bottom left octo
    if (leftX != -1 && bottomY != -1)
    {
        var o = octo.Where(o => o.X == leftX && o.Y == bottomY).First();
        o.Energy++;
        if (!o.Flashed &&
            o.Energy > 9)
        {
            Flash(o);
        }
    }

    // bottom octo
    if (bottomY != -1)
    {
        var o = octo.Where(o => o.X == x && o.Y == bottomY).First();
        o.Energy++;
        if (!o.Flashed &&
            o.Energy > 9)
        {
            Flash(o);
        }
    }

    // bottom right octo
    if (rightX != -1 && bottomY != -1)
    {
        var o = octo.Where(o => o.X == rightX && o.Y == bottomY).First();
        o.Energy++;
        if (!o.Flashed &&
            o.Energy > 9)
        {
            Flash(o);
        }
    }

    // right octo
    if (rightX != -1)
    {
        var o = octo.Where(o => o.X == rightX && o.Y == y).First();
        o.Energy++;
        if (!o.Flashed &&
            o.Energy > 9)
        {
            Flash(o);
        }
    }

    // top right octo
    if (rightX != -1 && topY != -1)
    {
        var o = octo.Where(o => o.X == rightX && o.Y == topY).First();
        o.Energy++;
        if (!o.Flashed &&
            o.Energy > 9)
        {
            Flash(o);
        }
    }

    // top octo
    if (topY != -1)
    {
        var o = octo.Where(o => o.X == x && o.Y == topY).First();
        o.Energy++;
        if (!o.Flashed &&
            o.Energy > 9)
        {
            Flash(o);
        }
    }
}

List<Octopus> GenerateOctopi(List<string> data)
{
    List<Octopus> octopi = new List<Octopus>();

    for (int x = 0; x < 10; x++)
    {
        for (int y = 0; y < 10; y++)
        {
            Octopus octopus = new Octopus();

            octopus.X = x;
            octopus.Y = y;
            octopus.Energy = int.Parse(data[y][x].ToString());
            octopus.Flashed = false;
            octopi.Add(octopus);
        }
    }

    return octopi;
}
public class Octopus
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Energy { get; set; }
    public bool Flashed { get; set; }
}