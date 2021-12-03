StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();

string[] diagnostics = data.Split("\r\n");
const int dataLength = 12; // 12 for data, 5 for test data
int[] gamma = new int[dataLength];
int[] epsillon = new int[dataLength];
int[] gammaPower = new int[dataLength];
int[] epsillonPower = new int[dataLength];
double g = 0;
double e = 0;
List<string> lines = new List<string>();
List<string> oxLines = new List<string>();
List<string> coLines = new List<string>();

foreach (var i in diagnostics)
{
    lines.Add(i.ToString());
    oxLines.Add(i.ToString());
    coLines.Add(i.ToString());
}

Part1();
Part2();

#region Part 1

void Part1()
{
    for (int i = 0; i < dataLength; i++)
    {
        int gammaCount;
        int epsillonCount;

        (gammaCount, epsillonCount) = FindMostCommon(lines, i);
        gamma[i] = gammaCount;
        epsillon[i] = epsillonCount;
    }

    SetBinaryPower();
    g = CalculatePower(gammaPower);
    e = CalculatePower(epsillonPower);

    Console.WriteLine($"The gamma power output is {g}. The epsillon power is {e}.  The total power consumption is {g * e}");
}

#endregion

#region Part 2

void Part2()
{
    int gammaCount;
    int epsillonCount;

    for (int i = 0; i < dataLength; i++)
    {
        (gammaCount, epsillonCount) = FindMostCommon(oxLines, i);

        if (gammaCount >= epsillonCount)
        {
            oxRemoveLines('0', i);
        }
        else
        {
            oxRemoveLines('1', i);
        }

        if (oxLines.Count == 1)
        {
            break;
        }
    }

    for (int i = 0; i < dataLength; i++)
    {
        (gammaCount, epsillonCount) = FindMostCommon(coLines, i);

        if (gammaCount < epsillonCount)
        {
            coRemoveLines('0', i);
        }
        else
        {
            coRemoveLines('1', i);
        }

        if (coLines.Count == 1)
        {
            break;
        }
    }

    int[] oxBinary = new int[dataLength];
    int[] coBinary = new int[dataLength];

    for (int i = 0; i < dataLength; i++)
    {
        if (oxLines.First()[i] == '1')
        {
            oxBinary[i] = 1;
        }
    }

    for (int i = 0; i < dataLength; i++)
    {
        if (coLines.First()[i] == '1')
        {
            coBinary[i] = 1;
        }
    }

    int oxRating = CalculatePower(oxBinary);
    int coRating = CalculatePower(coBinary);

    Console.WriteLine($"The oxygen rating is {oxRating}. The co2 rating is {coRating}.  The total life support is {oxRating * coRating}");
}

#endregion

#region Auxillary

(int, int) FindMostCommon(List<string> list, int position)
{
    int gammaCount = 0;
    int epsillonCount = 0;

    foreach (string line in list)
    {
        if (line[position] == '1')
        {
            gammaCount++;
        }
        else
        {
            epsillonCount++;
        }
    }

    return (gammaCount, epsillonCount);
}

void SetBinaryPower()
{
    for (int i = 0; i < dataLength; i++)
    {
        if (gamma[i] > epsillon[i])
        {
            gammaPower[i] = 1;
        }
        else
        {
            epsillonPower[i] = 1;
        }
    }
}

int CalculatePower(int[] rating)
{
    double r = 0;
    for (int i = 0; i < dataLength; i++)
    {
        r += rating[i] * Math.Pow(2, (dataLength - 1 - i));
    }
    return (int)r;
}

void oxRemoveLines(char value, int position)
{
    List<string> removeLines = new List<string>();

    foreach (var line in oxLines)
    {
        if (line[position] == value)
        {
            removeLines.Add(line);
        }
    }

    foreach (var line in removeLines)
    {
        oxLines.Remove(line);
    }
}

void coRemoveLines(char value, int position)
{
    List<string> removeLines = new List<string>();

    foreach (var line in coLines)
    {
        if (line[position] == value)
        {
            removeLines.Add(line);
        }
    }

    foreach (var line in removeLines)
    {
        coLines.Remove(line);
    }
}

#endregion

Console.ReadKey();