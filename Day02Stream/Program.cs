using Day02Stream;

SubmarinePositionService supPos = new SubmarinePositionService();

StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();

string[] depths = data.Split("\r\n");

var instructionList = supPos.ProcessData(depths);

int distance;
int depth;
int aim;
int tryAgainDepth;
int tryAgainDistance;
(distance, depth) = supPos.MoveTheSub(instructionList);
(tryAgainDistance, tryAgainDepth, aim) = supPos.TryAgain(instructionList);

Console.WriteLine($"Your submarine finished the instructions at {distance} units from the start, " +
                  $"and {depth} units deep.  The total returned value is {distance * depth}\r\n");

Console.WriteLine($"Your submarine finished the instructions at {tryAgainDistance} units from the start, " +
                  $"and {tryAgainDepth} units deep.  Your final aim was {aim}.  The total returned value is {tryAgainDistance * tryAgainDepth}");

Console.ReadKey();