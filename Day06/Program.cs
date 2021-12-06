using System.Text;

StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();

// part 1
// int days = 80;

// part 2
int days = 256;

long[] fishies = new long[9];

ProcessData();

for (int i = 0; i < days; i++)
{
    long babies = fishies[0];

    for (int j = 1; j < 9; j++)
    {
        fishies[j - 1] = fishies[j];
    }

    fishies[6] += babies;
    fishies[8] = babies;

}

Console.WriteLine($"In {days} days, there will be {fishies.Sum()} fish");
Console.ReadKey();

void ProcessData()
{
    var f = data.Split(",");

    foreach(var c in f)
    {
        var x = int.Parse(c.ToString());
        fishies[x]++;
    }
}