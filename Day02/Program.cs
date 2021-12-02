StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();

string[] depths = data.Split("\r\n");
int depth = 0;
int distance = 0;

// part 1
foreach(var d in depths)
{ 
    var change = d.Split(" ");
    var value = int.Parse(change[1]);
    if (string.Equals(change[0].ToLower(), "forward"))
    {
        distance += value;
    }
    else if (string.Equals(change[0].ToLower(), "down"))
    {
        depth += value;
    }
    else if (string.Equals(change[0].ToLower(), "up"))
    {
        depth -= value;
    }
}

// part 2
int p2Depth = 0;
int p2Distance = 0;
int aim = 0;

foreach (var d in depths)
{
    var change = d.Split(" ");
    var value = int.Parse(change[1]);
    if (string.Equals(change[0].ToLower(), "forward"))
    {
        p2Depth += value;
        p2Distance += value * aim;
    }
    else if (string.Equals(change[0].ToLower(), "down"))
    {
        aim += value;
    }
    else if (string.Equals(change[0].ToLower(), "up"))
    {
        aim -= value;
    }
}

Console.WriteLine($"Your ending depth is {depth} and your final distance is {distance} resulting in a total position of {depth * distance}");
Console.WriteLine($"Your revised ending depth is {p2Depth} and your final distance is {p2Distance} resulting in a total position of {p2Depth * p2Distance}");
