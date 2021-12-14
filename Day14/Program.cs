using System.Text;

StreamReader sr = File.OpenText(@"..\..\..\Data\Data.txt");
string data = sr.ReadToEnd();
var entries = data.Split("\r\n\r\n").ToList();

int iterations = 40; // 10 for part 1, 40 for part 2

Dictionary<string, long> letterCount = new Dictionary<string, long>();

var startingCompound = GetStartingDict(entries[0]);

var rules = GetRules(entries[1]);

var endingCompound = PerformRules(startingCompound, rules);

var orderedLetterCounts = letterCount.OrderBy(x => x.Value).ToList();

var mostCommon = orderedLetterCounts[orderedLetterCounts.Count - 1];
var leastCommon = orderedLetterCounts[0];

Console.WriteLine($"The letter with the highest count is {mostCommon.Key} with {mostCommon.Value} appearances.");
Console.WriteLine($"The letter with the highest count is {leastCommon.Key} with {leastCommon.Value} appearances.");
Console.WriteLine($"The difference between the two is {mostCommon.Value - leastCommon.Value}");
Console.ReadKey();

Dictionary<string, long> GetStartingDict(string entries)
{
    Dictionary<string, long> pairCount = new Dictionary<string, long>();

    foreach (var entry in entries)
    {
        if (!letterCount.ContainsKey($"{entry}"))
        {
            letterCount.Add($"{entry}", 1);
        }
        else
        {
            letterCount[$"{entry}"]++;
        }
    }

    for (var i = 1; i < entries.Length; i++)
    {
        if (!pairCount.ContainsKey($"{entries[i - 1]}{entries[i]}"))
        {
            pairCount.Add($"{entries[i - 1]}{entries[i]}", 1);
        }
        else
        {
            pairCount[$"{entries[i - 1]}{entries[i]}"]++;
        }
    }

    return pairCount;
}

Dictionary<string, long> PerformRules(Dictionary<string, long> compound, List<Rules> rules)
{
    for (int i = 0; i < iterations; i++)
    {
        compound = RulesCheck(compound, rules);
    }

    return compound;
}

Dictionary<string, long> RulesCheck(Dictionary<string, long> compound, List<Rules> rules)
{
    Dictionary<string, long> newCompound = new Dictionary<string, long>();

    foreach (var pair in compound)
    {
        var rule = rules.FirstOrDefault(x => x.Condition == pair.Key);

        foreach (var mod in rule.Modifications)
        {
            if (!newCompound.ContainsKey(mod))
            {
                newCompound.Add(mod, pair.Value);
            }
            else
            {
                newCompound[mod] += pair.Value;
            }
        }

        if (!letterCount.ContainsKey($"{rule.Modifications[1][0]}"))
        {
            letterCount.Add($"{rule.Modifications[1][0]}", pair.Value);
        }
        else
        {
            letterCount[$"{rule.Modifications[1][0]}"] += pair.Value;
        }
    }

    return newCompound;
}

List<Rules> GetRules(string data)
{
    List<Rules> rules = new List<Rules>();

    var ruleStrings = data.Split("\r\n").ToList();

    foreach (var ruleString in ruleStrings)
    {
        var details = ruleString.Split(" -> ");

        Rules rule = new Rules
        {
            Condition = details[0],
            Modifications = new List<string>
            {
                $"{details[0][0]}{details[1]}",
                $"{details[1]}{details[0][1]}"
            }
        };

        rules.Add(rule);
    }

    return rules;
}

public class Rules
{
    public string Condition { get; set; }
    public List<string> Modifications { get; set; }
}