StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();

string[] depths = data.Split("\r\n");
int increases = 0;
int tripleIncrease = 0;

// part 1
for(int i=1; i<depths.Length; i++)
{
    int oldDepth = int.Parse(depths[i - 1]);
    int newDepth = int.Parse(depths[i]); 

    if(newDepth > oldDepth)
    {
        increases++;
    }
}

// part 2
for(int i=3; i<depths.Length; i++)
{
    int prevTriple = 0;
    int newTriple = 0;

    int a = int.Parse(depths[i - 3]);
    int b = int.Parse(depths[i - 2]);
    int c = int.Parse(depths[i - 1]);
    int d = int.Parse(depths[i]);

    prevTriple = a + b + c;
    newTriple = b + c + d;

    if (newTriple > prevTriple)
    {
        tripleIncrease++;
    }
}

Console.WriteLine(increases);
Console.WriteLine(tripleIncrease);
Console.ReadKey();