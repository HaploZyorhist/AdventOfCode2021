StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();

string[] ventScan = data.Split("\r\n");

int[,] field = new int[1000000, 1000000];

Console.WriteLine("Hello, World!");

List<Line> ProcessData(string[] data)
{
    List<Line> lines = new List<Line>();

    foreach (string d in data)
    {
        Line line = new Line();



        lines.Add(line);
    }

    return lines;
}
class Line
{
    int X1 { get; set; }
    int X2 { get; set; }
    int Y1 { get; set; }
    int Y2 { get; set; }
}