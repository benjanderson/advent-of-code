<Query Kind="Program" />

void Main()
{
	var input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), @"input.txt"));
	var depth = 0;
	var horiz = 0;
	var aim = 0;
	foreach (var line in input)
	{
		var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		var dir = split[0];
		var amount = Convert.ToInt32(split[1]);
		switch (dir)
		{
			case "forward":
				horiz += amount;
				depth += aim * amount;
				break;
			case "down":
				//depth += amount;
				aim += amount;
				break;
			case "up":
				//depth -= amount;
				aim -= amount;
				break;
			default:
				throw new InvalidOperationException($"{line} contains invalid direction");
		}
	}
	
	depth.Dump("depth");
	horiz.Dump("horizantal");
	var solution1 = depth * horiz;
	solution1.Dump("solution");
	
}

