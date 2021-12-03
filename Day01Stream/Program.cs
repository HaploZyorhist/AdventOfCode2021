using Day01Stream;

DataProcessingService ds = new DataProcessingService();

StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();

string[] depths = data.Split("\r\n");

List<int> dList = ds.ConvertToInts(depths);

var depthIncreaseCount = ds.Part1Processing(dList);
var depthTripleCount = ds.Part2Processing(dList);

Console.WriteLine($"The depth of the sea has increased {depthIncreaseCount} times");
Console.WriteLine($"The depth of the sea has increased {depthTripleCount} times for triple measurements");
Console.ReadKey();