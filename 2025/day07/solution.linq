<Query Kind="Program" />

string[] input;

void Main()
{
	input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), @"input.txt"));
	//input = new[] {
	//	".......S.......",
	//	"...............",
	//	".......^.......",
	//	"...............",
	//	"......^.^......",
	//	"...............",
	//	".....^.^.^.....",
	//	"...............",
	//	"....^.^...^....",
	//	"...............",
	//	"...^.^...^.^...",
	//	"...............",
	//	"..^...^.....^..",
	//	"...............",
	//	".^.^.^.^.^...^.",
	//	"..............."};
	var output = new List<string[]>();
 
	Laser[] lasers = Enumerable.Range(0, input[0].Length).Select(e => new Laser { Type = Cell.Blank, NumTimelines = 0}).ToArray();
	lasers[input[0].IndexOf('S')].Type = Cell.Laser;
	lasers[input[0].IndexOf('S')].NumTimelines = 1;
	
	output.Add(lasers.Select(l => l.Type == Cell.Laser ? "0" : l.Type == Cell.Split ? "^" : ".").ToArray());
	var numSplits = 0;
	for (int y = 1; y < input.Length; y++)
	{
		for (int x = 0; x < input[y].Length; x++)
		{
			if (lasers[x].Type == Cell.Laser && input[y][x] == '^')
			{
				if (x <= 0 || x >= input[y].Length)
				{
					throw new InvalidOperationException("This shouldn't happen!");
				}

				numSplits++;	
				lasers[x - 1].Type = Cell.Laser;
				lasers[x - 1].NumTimelines = lasers[x].NumTimelines + lasers[x - 1].NumTimelines;
				lasers[x + 1].Type = Cell.Laser;
				lasers[x + 1].NumTimelines = lasers[x].NumTimelines + lasers[x + 1].NumTimelines;

				lasers[x].Type = Cell.Split;
				lasers[x].NumTimelines = 0;
			}
			else if (lasers[x].Type == Cell.Split)
			{
				lasers[x].Type = Cell.Blank;
			}
		}
		
		output.Add(lasers.Select(l => l.Type == Cell.Laser ? l.NumTimelines.ToString() : l.Type == Cell.Split ? "^" : ".").ToArray());
	}
	numSplits.Dump("num splits");
	lasers.Sum(l => l.NumTimelines).Dump("Num timelines");
	Display(output);
}

public class Laser
{
	public Cell Type {get;set;}
	public long NumTimelines{get; set;}
}
public enum Cell {Blank, Laser, Split};

public static string ReplaceAtIndex(string text, int index, char newChar)
{
	if (string.IsNullOrEmpty(text))
	{
		throw new ArgumentException("String cannot be null or empty", nameof(text));
	}

	if (index < 0 || index >= text.Length)
	{
		throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
	}

	char[] chars = text.ToCharArray();

	chars[index] = newChar;

	return new string(chars);
}

public void Display(List<string[]> arr)
{
	Util.RawHtml("<table><tbody>" + string.Join("", arr.Select(row => "<tr>" + string.Join("", row.Select(td => "<td>" + td + "</td>")) + "</tr>"))).Dump();
}


