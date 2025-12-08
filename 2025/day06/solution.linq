<Query Kind="Program" />

void Main()
{
	var input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), @"input.txt"));
//	var input = new[] {
//	"123 328  51 64 ",
// 	" 45 64  387 23 ",
//  	"  6 98  215 314",
//	"*   +   *   +  "};
	
	//PartOne(input);
	
	var data = SplitColumns(input);
	var equations = new List<Equation>();
	var height = input.Length - 1;
	foreach (var row in data)
	{
		var newEq = new Equation
		{
			Operation = row.Last().Trim(),
		};
		for (int x = row[0].Length - 1; x >= 0 ; x--)
		{
			row.Dump();
			var chars = Enumerable.Range(0, height).Select(y => row[y].ElementAt(x)).ToArray();
			chars.Dump();
			newEq.Numbers.Add(long.Parse(new string(chars).Trim().ToArray()));
		}
		equations.Add(newEq);
	}
	//equations.Dump();
	equations.Sum(e => e.Total()).Dump("Grand Total");
}

public class Equation 
{
	public string Operation {get;set;}
	public List<long> Numbers = new List<long>();
	
	public long Total()
	{
		if (!Numbers.Any() || string.IsNullOrWhiteSpace(Operation))
		{
			return 0;
		}
		if (Operation == "*")
		{
			var total = Numbers.First();
			Numbers.Skip(1).ToList().ForEach(n => total = total * n);
			return total;
		}
		if (Operation == "+")
		{
			return Numbers.Sum();
		}
		throw new NotImplementedException("I dont know this operation: " + Operation);
	}
}

List<string[]> SplitColumns(string[] input)
{
	if (input.Length == 0)
	{
		throw new InvalidOperationException("input is empty");
	}
	var numEquations = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
	var width = input[0].Length;
	var height = input.Length;
	
	var data = new List<string[]>();//Enumerable.Range(0, numEquations).Select(e => new int[height]).ToList();
	
	var lastNumberIndex = 0;
	for (int x = 0; x < width; x++)
	{
		// find the first space of each line
		var isSpace = true;
		for (int y = 0; y < height; y++)
		{
			if (!char.Equals(input[y][x], ' '))
			{
				isSpace = false;
			}
		}

		if (isSpace && x != lastNumberIndex)
		{
			data.Add(ExtractEquation(input, x, lastNumberIndex, height));
			lastNumberIndex = x;
		}
	}

	// get last one
	data.Add(ExtractEquation(input, width - 1, lastNumberIndex, height));
	
	//data.Dump();
	return data;
}

string[] ExtractEquation(string[] input, int x, int lastNumberIndex, int height)
{
	var arr = new string[height];
	for (int y = 0; y < height; y++)
	{
		arr[y] = input[y].Substring(lastNumberIndex, (x + 1) - lastNumberIndex);
	}
	if (arr.All(a => a.StartsWith(" ")))
	{
		arr = arr.Select(a => a.Substring(1)).ToArray();
	}
	if (arr.All(a => a.EndsWith(" ")))
	{
		arr = arr.Select(a => a.Substring(0, a.Length - 1)).ToArray();
	}
	return arr;
}

void PartOne(string[] input)
{
	var split = input.Select(i => i.Split(" ", StringSplitOptions.None)).ToArray();
	var height = split[0].Length;
	var width = input.Length;
	var data = new List<string[]>();
	for (int i = 0; i < height; i++)
	{
		var arr = new string[width];
		for (int j = 0; j < width; j++)
		{
			arr[j] = split[j][i];
		}
		data.Add(arr);
	}

	long grandTotal = 0;
	for (int i = 0; i < height; i++)
	{
		var nums = data[i].Take(data[i].Count() - 1).Select(s => long.Parse(s)).ToList();
		if (nums.Any(n => n < 0))
		{
			data[i].Dump();
			throw new InvalidOperationException();
		}
		long total = 0;
		switch (data[i].Last())
		{
			case "*":
				total = nums.First();
				nums.Skip(1).ToList().ForEach(n => total = total * n);
				break;
			case "+":
				total = nums.Sum();
				break;
			default:
				throw new NotImplementedException("i donno this operation: " + data[i].Last());
		}
		total.Dump("total");
		grandTotal += total;
	}
	grandTotal.Dump("grand total");
}