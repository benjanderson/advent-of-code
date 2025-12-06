<Query Kind="Program" />

void Main()
{
	var input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath),  @"input.txt"));
	var first = new Regex(@"(?<color>[a-z]+\s[a-z]+)\sbags\scontain\s(?<target>.+)\.");
	var second = new Regex(@"(?<number>\d)\s(?<color>[a-z]+\s[a-z]+)\sbags");
	
	var matches = input
	.Select(i => first.Match(i))
	.Select(i => new { Color = i.Groups["color"].Value, Target = i.Groups["target"].Value })
	.Select(i =>
	{
		var matches = second.Matches(i.Target);
		return new Match
		{
			Color = i.Color,
			Targets = matches.Select(m => new Target { Number = Convert.ToInt32(m.Groups["number"].Value), Color = m.Groups["color"].Value }).ToList()
		};
	})
	.ToDictionary(k => k.Color, k => k.Targets.ToDictionary(a => a.Color, a => a.Number));
	
	
	var shinyGoldBags = new HashSet<string>();
	var solution = Part1(matches);
	
	solution.Dump("Solution");//WRONG!?!?!?!
	//matches.Dump();
	
}

private const string Colour = "shiny gold";
string Part1(Dictionary<string, Dictionary<string, int>> input)
{
	bool Contains(string colour) =>
		input[colour].ContainsKey(Colour) || input[colour].Keys.Any(Contains);
	return input.Keys.Count(Contains).ToString();
}

//public void Bags(string color, List<Match> matches, ref HashSet<string> innerBags, int depth)
//{
//	if (depth > 0 && !innerBags.Contains(color))
//	{
//		innerBags.Add(color);
//		$"{new string('-', depth)}{color} - {innerBags.Count}".Dump();
//	}
//	
//	
//	if (depth == 100)
//	{
//		throw new InvalidOperationException("Too many recursive calls");
//	}
//
//	
//	foreach(var bag in matches.Where(m => m.Targets.Any(t => t.Color == color)))
//	{
//		Bags(bag.Color, matches, ref innerBags, depth+1);
//	}
//}

public class Match
{
	public string Color {get; set;}
	public List<Target> Targets {get; set;} = new List<Target>();
	public bool CanContainShinyGold {get; set;} = false;
}

public class Target
{
	public int Number {get; set;}
	public string Color{get;set;}
}
