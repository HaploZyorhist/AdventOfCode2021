StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();
var postions = data.Split(",").Select(Int32.Parse).ToList();

#region Part 1
int numberCount = postions.Count();
int halfIndex = postions.Count() / 2;
var sortedNumbers = postions.OrderBy(x => x);
double median;
int fuel = 0;
int modifiedFuel = 0;

if (numberCount % 2 == 0)
{
    median = (sortedNumbers.ElementAt(halfIndex) + sortedNumbers.ElementAt(halfIndex - 1)) / 2;
}
else
{
    median = sortedNumbers.ElementAt(halfIndex);
}

foreach(var pos in postions)
{
    var consumption = Math.Abs(pos - median);

    fuel += (int)consumption;
}

#endregion

#region Part 2

double avg = Queryable.Average(postions.AsQueryable());

int a = (int)avg;

foreach(var pos in postions)
{
    var consumption = Math.Abs(pos - a);

    for (int i = 0; i < consumption; i++)
    {
        modifiedFuel += i + 1;
    }
}

Console.WriteLine($"The crabs should position themselves at {median} which would consume {fuel} fuel.");
Console.WriteLine($"The crabs should position themselves at {a} which would consume {modifiedFuel} fuel.");
Console.ReadKey();

#endregion