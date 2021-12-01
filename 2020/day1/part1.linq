<Query Kind="Statements" />

var input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath),  @"input.txt"));
var numbers = input.Select(i => Convert.ToInt32(i)).ToList();
int first = -1, second = -1;
for (int i = 0; i < numbers.Count; i++)
{
	for (int j = 0; j < numbers.Count; j++)
	{
		if (i==j)
		{
			continue;
		}
		
		if (numbers[i] + numbers[j] == 2020)
		{
			first = numbers[i];
			second = numbers[j];
			break;
		}
	}
}

first.Dump("first");
second.Dump("second");
(first * second).Dump("solution");