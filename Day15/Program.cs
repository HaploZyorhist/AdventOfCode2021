StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();
var entries = data.Split("\r\n").ToList();

var length = entries[0].Length * 5; // part 1 with no multiplier, * 5 for part 2
var height = entries.Count() * 5; // part 1 with no multiplier, * 5 for part 2
int totalCost = 0;

var riskMap = new Square[length, height];

ProcessData();

var startSquare = riskMap[0, 0];
var endSquare = riskMap[length - 1, height - 1];

NavigateField();

Console.WriteLine($"The least expensive path has a risk of {totalCost} to traverse");
Console.ReadKey();

void NavigateField()
{
    Dictionary<Square, int> openSquares = new Dictionary<Square, int>();
    Dictionary<Square, int> closeSquares = new Dictionary<Square, int>();

    openSquares.Add(startSquare, 0);

    while (true)
    {
        Square currentSquare = null;

        foreach(var s in openSquares)
        {
            if (currentSquare == null)
            {
                currentSquare = s.Key;
            }
            else if (s.Key == endSquare)
            {
                currentSquare = s.Key;
            }
            else if (s.Key.FCost < currentSquare.FCost)
            {
                currentSquare = s.Key;
            }
        }

        if (currentSquare != null)
        {
            openSquares.Remove(currentSquare);
            closeSquares.Add(currentSquare, 0);
        }
        else
        {
            Console.WriteLine("breaking error, something went wrong");
            break;
        }
        if (currentSquare == endSquare)
        {
            break;
        }

        foreach(var s in currentSquare.Neighbors)
        {
            if(closeSquares.ContainsKey(s))
            {
                continue;
            }

            if(!openSquares.ContainsKey(s))
            {
                s.SetSquare(currentSquare, endSquare);
                openSquares.Add(s, 0);
            }
            else if (s.CheckDifference(currentSquare, endSquare))
            {
                s.SetSquare(currentSquare, endSquare);
            }
        }
    }

    Square current = endSquare;
    while(current != startSquare)
    {
        totalCost += current.Cost;
        current = current.Parent;
    }
}

void ProcessData()
{
    for (int x = 0; x < length; x++)
    {
        for (int y = 0; y < height; y++)
        {
            int xOffset = x % entries[0].Length;
            int yOffset = y % entries.Count;

            var value = int.Parse(entries[yOffset][xOffset].ToString());

            int xCostAdjust = (x - xOffset) / entries[0].Length;
            int yCostAdjust = (y - yOffset) / entries.Count;

            int newValue = (value + xCostAdjust + yCostAdjust) % 9;

            if (newValue == 0)
            {
                newValue = 9;
            }

            Square s = new Square
            {
                Position = new Vector2(x, y),
                Cost = newValue,
                Parent = null,
                Neighbors = new List<Square>()
            };
            riskMap[x, y] = s;
        }
    }

    for (int x = 0; x < length; x++)
    {
        for (int y = 0; y < height; y++)
        {
            var n = GetNeighbors(x, y);

            riskMap[x, y].Neighbors.AddRange(n);
        }
    }
}

List<Square> GetNeighbors(int x, int y)
{
    List<Square> neighbors = new List<Square>();

    if (x != 0)
    {
        int newX = x - 1;
        int newY = y;
        var s = riskMap[newX, newY];
        neighbors.Add(s);
    }
    if (y != 0)
    {
        int newX = x;
        int newY = y - 1;
        var s = riskMap[newX, newY];
        neighbors.Add(s);
    }
    if (x < length - 1)
    {
        int newX = x + 1;
        int newY = y;
        var s = riskMap[newX, newY];
        neighbors.Add(s);
    }
    if (y < height - 1)
    {
        int newX = x;
        int newY = y + 1;
        var s = riskMap[newX, newY];
        neighbors.Add(s);
    }

    return neighbors;
}

public class Vector2
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Square
{
    public Vector2 Position { get; set; }
    public int Cost { get; set; }
    public Square Parent { get; set; }
    public List<Square> Neighbors { get; set; }

    // i dont fully understand the g, h, and f cost things but they are used 
    // to calculate the total cost over distance traveled
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost { get; set; }

    public void SetStartSquare(Square target)
    {
        GCost = 0;
        HCost = Cost;
        FCost = CalculateFCost(GCost, HCost);
    }

    public void SetSquare(Square parent, Square target)
    {
        Parent = parent;
        GCost = parent.GCost + Cost;
        HCost = parent.HCost + Cost;
        FCost = CalculateFCost(GCost, HCost);
    }

    public bool CheckDifference(Square parent, Square target)
    {
        if (CalculateFCost(parent.GCost + Cost, parent.HCost + Cost) < FCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int CalculateFCost(int g, int h)
    {
        int f = g + h;
        return f;
    }
}