StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();
var entries = data.Split("\r\n").ToList();

int errorPoints = 0;
List<long> correctionPoints = new List<long>();

foreach (var entry in entries)
{
    var c = CheckForError(entry);

    if (c == null)
    {
        var x = FixLine(entry);

        correctionPoints.Add(x);
    }

    else if (c != null)
    {
        var x = GetPoints((char)c);

        errorPoints += x;
    }
}

correctionPoints.Sort();
int skip = correctionPoints.Count / 2;
long middleScore = correctionPoints.Skip(skip).First();


Console.WriteLine($"The total points for the illegal characters is {errorPoints}");
Console.WriteLine($"The total points for corrected characters is {middleScore}");
Console.ReadKey();

char? CheckForError(string line)
{
    Stack<int> openingStack = new Stack<int>();

    for (int i = 0; i < line.Length; i++)
    {
        int index = MapCharacter(line[i]);

        if (index < 4)
        {
            openingStack.Push(index);
        }
        else
        {
            index = index % 4;
            if (openingStack.Pop() != index)
            {
                return line[i];
            }
        }
    }

    return null;
}

long FixLine(string line)
{
    Stack<int> openingStack = new Stack<int>();

    for (int i = 0; i < line.Length; i++)
    {
        int index = MapCharacter(line[i]);

        if (index < 4)
        {
            openingStack.Push(index);
        }
        else
        {
            openingStack.Pop();
        }
    }

    var x = CalculateErrors(openingStack);
    return x;
}

long CalculateErrors(Stack<int> openingStack)
{
    long score = 0;

    foreach (int item in openingStack)
    {
        score = score * 5;
        score += item + 1;
    }

    return score;
}

int MapCharacter(char ch)
{
    switch (ch)
    {
        case '(':
            return 0;

        case '[':
            return 1;

        case '{':
            return 2;

        case '<':
            return 3;

        case ')':
            return 4;

        case ']':
            return 5;

        case '}':
            return 6;

        case '>':
            return 7;
    }

    return -1;
}

int GetPoints(char ch)
{
    switch (ch)
    {
        case ')':
            return 3;

        case ']':
            return 57;

        case '}':
            return 1197;

        case '>':
            return 25137;

        default:
            return 0;
    }
}