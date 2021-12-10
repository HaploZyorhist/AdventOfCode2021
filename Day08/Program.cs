using System.Text;

StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();
var entries = data.Split("\r\n").ToList();

int oneCount = 0;
int fourCount = 0;
int sevenCount = 0;
int eightCount = 0;
int totalUnique = 0;

foreach (var entry in entries)
{
    var d = entry.Split("|");

    var reading = d[1].Trim().Split(" ");

    foreach (var read in reading)
    {
        switch (read.Length)
        {
            case 2:
                oneCount++;
                break;

            case 3:
                sevenCount++;
                break;

            case 4:
                fourCount++;
                break;

            case 7:
                eightCount++;
                break;
        }
    }
}

totalUnique = oneCount + fourCount + sevenCount + eightCount;

Console.WriteLine($"The number of times 1, 4, 7, and 8 appear is {totalUnique} times");

// part 2

int totalCount = 0;

foreach (var entry in entries)
{
    var toMap = entry.Split("|");
    var map = Mapper(toMap[0]);

    var display = toMap[1].Split(" ");

    StringBuilder sb = new StringBuilder();

    foreach (var d in display)
    {
        string digit = map.GetValueOrDefault(string.Concat(d.OrderBy(x => x))).ToString();
        sb.Append(digit);
    }

    int num = int.Parse(sb.ToString());
    totalCount += num;
}

Console.WriteLine($"The total of all displays is {totalCount}");
Console.ReadKey();

Dictionary<string, int> Mapper(string map)
{
    var digit = map.Split(" ");

    string one = "";
    string two = "";
    string three = "";
    string four = "";
    string five = "";
    string six = "";
    string seven = "";
    string eight = "";
    string nine = "";
    string zero = "";

    List<string> fiveLengthInput = new List<string>();
    List<string> sixLengthInput = new List<string>();

    one = digit.Where(x => x.Length == 2).First();
    four = digit.Where(x => x.Length == 4).First();
    seven = digit.Where(x => x.Length == 3).First();
    eight = digit.Where(x => x.Length == 7).First();

    fiveLengthInput = digit.Where(x => x.Length == 5).ToList();
    sixLengthInput = digit.Where(x => x.Length == 6).ToList();

    foreach (var num in sixLengthInput)
    {
        if (CheckChars(num, four))
        {
            nine = num;
            continue;
        }
        if (CheckChars(num, one))
        {
            zero = num;
            continue;
        }
        six = num;
    }

    char Top = FindMissingInput(seven, one);
    char Mid = FindMissingInput(eight, zero);
    char tRight = FindMissingInput(eight, six);
    char bLeft = FindMissingInput(eight, nine);
    char bRight = FindMissingInput(one, $"{tRight}");
    char tLeft = FindMissingInput(four, $"{tRight}{Mid}{bRight}");
    char Bot = FindMissingInput(eight, $"{Top}{Mid}{tRight}{tLeft}{bRight}{bLeft}");

    foreach (var num in fiveLengthInput)
    {
        if (CheckChars(num, $"{Top}{Mid}{Bot}{tRight}{bLeft}"))
        {
            two = num;
            continue;
        }
        if (CheckChars(num, $"{Top}{Mid}{Bot}{tRight}{bRight}"))
        {
            three = num;
            continue;
        }
        five = num;
    }

    Dictionary<string, int> mapReturn = new Dictionary<string, int>
    {
        {string.Concat(zero.OrderBy(c => c)), 0 },
        {string.Concat(one.OrderBy(c => c)), 1},
        {string.Concat(two.OrderBy(c => c)), 2},
        {string.Concat(three.OrderBy(c => c)), 3},
        {string.Concat(four.OrderBy(c => c)), 4},
        {string.Concat(five.OrderBy(c => c)), 5},
        {string.Concat(six.OrderBy(c => c)), 6},
        {string.Concat(seven.OrderBy(c => c)), 7},
        {string.Concat(eight.OrderBy(c => c)), 8},
        {string.Concat(nine.OrderBy(c => c)), 9}
    };

    return mapReturn;
}

bool CheckChars(string number, string map)
{
    foreach (char c in map)
    {
        if (!number.Contains(c))
        {
            return false;
        }
    }
    return true;
}

char FindMissingInput(string mostInputs, string leastInputs)
{
    foreach (var c in mostInputs)
    {
        if (!leastInputs.Contains(c))
        {
            return c;
        }
    }
    return ',';
}