<Query Kind="Program" />

void Main()
{
	var input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), @"input.txt"));
	//var input = new []{"0123456"};
	//PartOne(input);
	PartTwo(input, 12);

	//Assert(56, Jolt("0123456", 2));
	//Assert(987654321111, Jolt("987654321111111", 12));
	
	//Assert(811111111119, Jolt("811111111111119", 12));
	//Assert(434234234278, Jolt("234234234234278", 12));
	//Assert(434, Jolt("4234", 3));
	//Assert(888911112111, Jolt("818181911112111", 12));
}

public void PartOne(string[] input)
{
	var total = 0;
	foreach (var line in input)
	{
		short first = 0;
		short second = 0;
		for (int i = 0; i < line.Length - 1; i++)
		{
			var num = short.Parse(line[i].ToString());
			if (num > first)
			{
				first = num;
				second = short.Parse(line[i + 1].ToString());
			}
			else if (num > second)
			{
				second = num;
			}
		}
		var last = short.Parse(line[line.Length - 1].ToString());
		if (last > second)
		{
			second = last;
		}

		$"{first} {second} {line}".Dump();
		var combined = (first * 10) + second;
		total += combined;
	}

	total.Dump("total");
}

public void PartTwo(string[] input, int length)
{
	long total = 0;
	foreach (var line in input)
	{
		var combined = Jolt(line, length);
		//combined.Dump("combined");
		total += combined;
	}

	total.Dump("total");
}

long Jolt(string line, int length)
{
	//$"line = {line} length = {length}".Dump();
	var numLine = line.Select(l => short.Parse(l.ToString())).ToArray();
	var joltage = new int[length];
	for (int i = 0; i <= numLine.Length - joltage.Length; i++)
	{
		var numBatteriesRemaining = numLine.Length - i;
		//var limit = Math.Min(numBatteriesRemaining - joltage.Length, 0) * -1;
		//limit.Dump("limit");
		for (int j = 0, i2 = i; j < joltage.Length; j++, i2++)
		{
			//$"numLine[{i2}] {numLine[i2]} > joltage[{j}] {joltage[j]} --- {string.Join("", joltage.Select(l => l.ToString()))}".Dump();
			if (numLine[i2] > joltage[j])
			{
				//$"[{j2}] {joltage[j2]} = [{i}] {numLine[i]}".Dump();
				//joltage[j] = numLine[i2];
				for (int k = j, k2 = i2; k < joltage.Length; k++, k2++)
				{
					//$"REPLACING joltage[{k}] {joltage[k]} = numLine[{k2}] {numLine[k2]} --- k:{k} k2:{k2}".Dump();
					joltage[k] = numLine[k2];
				}
				break;
			}
		}
	}

	//$"{string.Join(" ", joltage.Select(l => l.ToString()))} {line}".Dump();
	long combined = long.Parse(string.Join("", joltage));
	return combined;
}

void Assert(long expected, long actual)
{
	$"expected {expected} {(long.Equals(expected, actual) ? "equals" : "does not equal")} {actual} actual".Dump();
}