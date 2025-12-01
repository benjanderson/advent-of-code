<Query Kind="Program" />

void Main()
{
	var numRegex = new Regex(@"\d+");
	var input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), @"input.txt"));
	var instructions = input.Select(i => new { Dir = i.StartsWith("R") ? Direction.Up : Direction.Down, Num = int.Parse(numRegex.Match(i).Value)});
	var position = 50;
	var zeroLandCount = 0;
	var zeroPassCount = 0;
	
	foreach (var line in instructions)
	{
		var lineZeroPassCount = 0;
		var original = position;
		position += line.Dir == Direction.Up ? line.Num : (line.Num * -1);
		foreach(var pos in Range(original, position))
		{
			if (pos % 100 == 0)
			{
				lineZeroPassCount++;
			}
		}
		position = position % 100;

		if (position == 0)
		{
			zeroLandCount++;
		}
		zeroPassCount += lineZeroPassCount;
		
		$"{original} {(line.Dir == Direction.Up ? "+" : "-")} {line.Num} = {position}. Passed zero {lineZeroPassCount} times".Dump();
	}
	zeroLandCount.Dump("Zero Land Count");
	zeroPassCount.Dump("Zero Pass Count");
}

public IEnumerable<int> Range(int start, int end)
{
	if (end > start)
	{
		for(var i = start + 1; i <= end; i++)
		{
			yield return i;
		}
	}
	else
	{
		for (var i = start - 1; i >= end; i--)
		{
			yield return i;
		}
	}
}

enum Direction 
{
	Up,
	Down
}
